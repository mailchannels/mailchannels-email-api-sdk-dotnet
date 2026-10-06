using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using MailChannels.EmailApi.Api;
using MailChannels.EmailApi.Extensions;
using MailChannels.EmailApi.Model;
using Microsoft.Extensions.DependencyInjection;

static class TlsProbe
{
    private static X509Certificate2 ServerCertificate(X509Certificate2 certificate)
    {
        // Schannel cannot reliably serve an ephemeral CopyWithPrivateKey key.
        // Reimport this synthetic test certificate with the default key storage;
        // Dispose cleans it up. Never install a root in the machine trust store.
        if (!OperatingSystem.IsWindows()) return new X509Certificate2(certificate);
        var pfx = certificate.Export(X509ContentType.Pfx);
#if NET9_0_OR_GREATER
        return X509CertificateLoader.LoadPkcs12(pfx, null, X509KeyStorageFlags.DefaultKeySet);
#else
        return new X509Certificate2(pfx, (string?)null, X509KeyStorageFlags.DefaultKeySet);
#endif
    }

    public static async Task<int> RunAsync()
    {
        using var rootKey = RSA.Create(2048);
        var rootRequest = new CertificateRequest("CN=MailChannels fixture root", rootKey, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        rootRequest.CertificateExtensions.Add(new X509BasicConstraintsExtension(true, false, 0, true));
        rootRequest.CertificateExtensions.Add(new X509KeyUsageExtension(X509KeyUsageFlags.KeyCertSign, true));
        using var root = rootRequest.CreateSelfSigned(DateTimeOffset.UtcNow.AddDays(-2), DateTimeOffset.UtcNow.AddDays(2));
        foreach (var scenario in new[] {"302", "307", "untrusted", "valid", "hostname", "expired"})
        {
            int status = scenario == "302" ? 302 : scenario == "307" ? 307 : 200;
            bool reject = scenario is "untrusted" or "hostname" or "expired";
            using var key = RSA.Create(2048);
            var request = new CertificateRequest("CN=localhost", key, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            var san = new SubjectAlternativeNameBuilder(); san.AddDnsName(scenario == "hostname" ? "wrong.example.invalid" : "localhost");
            request.CertificateExtensions.Add(san.Build());
            request.CertificateExtensions.Add(new X509BasicConstraintsExtension(false, false, 0, true));
            request.CertificateExtensions.Add(new X509EnhancedKeyUsageExtension(new OidCollection {new Oid("1.3.6.1.5.5.7.3.1")}, true));
            using var publicCert = request.Create(root, DateTimeOffset.UtcNow.AddDays(-1), scenario == "expired" ? DateTimeOffset.UtcNow.AddHours(-1) : DateTimeOffset.UtcNow.AddHours(1), RandomNumberGenerator.GetBytes(16));
            using var ephemeralCert = publicCert.CopyWithPrivateKey(key);
            using var cert = ServerCertificate(ephemeralCert);
            using var stop = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            var listener = new TcpListener(IPAddress.Loopback, 0); listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            int received = 0;
            var serve = Task.Run(async () => {
                try {
                    while (!stop.IsCancellationRequested) {
                        using var tcp = await listener.AcceptTcpClientAsync(stop.Token);
                        using var tls = new SslStream(tcp.GetStream());
                        try { await tls.AuthenticateAsServerAsync(new SslServerAuthenticationOptions { ServerCertificate = cert }, stop.Token); }
                        catch (AuthenticationException) when (reject) { continue; }
                        using var reader = new StreamReader(tls, Encoding.ASCII, false, 1024, true);
                        var first = await reader.ReadLineAsync(stop.Token);
                        if (first is null) continue;
                        int length = 0; string? line;
                        while (!string.IsNullOrEmpty(line = await reader.ReadLineAsync(stop.Token)))
                            if (line.StartsWith("Content-Length:", StringComparison.OrdinalIgnoreCase)) length = int.Parse(line.Split(':')[1]);
                        var body = new char[length]; int read = 0;
                        while (read < length) { var n = await reader.ReadAsync(body.AsMemory(read), stop.Token); if (n == 0) break; read += n; }
                        received++;
                        var response = received == 1 && status != 200
                            ? $"HTTP/1.1 {status} Redirect\r\nLocation: https://localhost:{port}/leaked\r\nContent-Length: 0\r\nConnection: close\r\n\r\n"
                            : "HTTP/1.1 200 OK\r\nContent-Length: 11\r\nConnection: close\r\n\r\n{\"data\":[]}";
                        await tls.WriteAsync(Encoding.ASCII.GetBytes(response), stop.Token);
                    }
                } catch (OperationCanceledException) { }
                  catch (IOException) when (reject) { }
            });
            var services = new ServiceCollection(); services.AddLogging();
            services.AddApi(c => c.AddApiHttpClients(h => h.BaseAddress = new Uri($"https://localhost:{port}/tx/v1"), b => {
                if (scenario != "untrusted") b.ConfigurePrimaryHttpMessageHandler((handler, _) => {
                    // Private trust applies only to this handler. The platform still
                    // verifies hostname, chain and validity; no validation callback.
                    var policy = new X509ChainPolicy { TrustMode = X509ChainTrustMode.CustomRootTrust, RevocationMode = X509RevocationMode.NoCheck };
                    policy.CustomTrustStore.Add(root);
                    ((SocketsHttpHandler)handler).SslOptions.CertificateChainPolicy = policy;
                });
            }));
            try {
                using var provider = services.BuildServiceProvider();
                var body = new MailSendBody(new() {new("text/plain", "fixture")}, new("a@example.invalid"), new() {new(new() {new("b@example.invalid")})}, "fixture");
                bool rejected = false;
                try {
                    var response = await provider.GetRequiredService<ISendApi>().SendEmailAsync("fixture-key", body, cancellationToken: stop.Token);
                    if ((int)response.StatusCode != status) throw new Exception("Redirect was followed");
                } catch (HttpRequestException) when (reject) { rejected = true; }
                if (reject && (!rejected || received != 0)) throw new Exception("Invalid certificate allowed HTTP transmission");
                if (!reject && received != 1) throw new Exception("Unexpected request count");
            } finally {
                stop.Cancel(); listener.Stop(); await serve;
            }
            Console.WriteLine(reject ? $"PASS {scenario} TLS rejects before HTTP transmission" : $"PASS local TLS {scenario} returns status {status} with one request");
        }
        return 6;
    }
}
