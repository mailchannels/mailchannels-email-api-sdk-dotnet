using System.Net;
using MailChannels.EmailApi.Api;
using MailChannels.EmailApi.Extensions;
using MailChannels.EmailApi.Model;
using Microsoft.Extensions.DependencyInjection;

static class TransportProbe
{
    static void Require(bool value, string message) { if (!value) throw new Exception(message); }
    static MailSendBody Body() => new(new() {new("text/plain", "fixture")}, new("a@example.invalid"), new() {new(new() {new("b@example.invalid")})}, "fixture");
    public static async Task<int> RunAsync()
    {
        var services = new ServiceCollection(); services.AddLogging(); services.AddApi();
        using (var provider = services.BuildServiceProvider())
        {
            foreach (var name in new[] {"ICustomTrackingApi", "IDKIMApi", "IMetricsApi", "ISendApi", "ISubAccountsApi", "ISuppressionApi", "IUsageApi", "IWebhooksApi"})
            {
                var key = "MailChannels.EmailApi.Api." + name;
                var client = provider.GetRequiredService<IHttpClientFactory>().CreateClient(key);
                Require(client.Timeout == TimeSpan.FromSeconds(30), name + " total timeout");
                var handler = provider.GetRequiredService<IHttpMessageHandlerFactory>().CreateHandler(key);
                while (handler is DelegatingHandler d) handler = d.InnerHandler!;
                Require(handler is SocketsHttpHandler, name + " explicit transport");
                var socket = (SocketsHttpHandler)handler;
                Require(!socket.AllowAutoRedirect, name + " redirects disabled");
                Require(socket.ConnectTimeout == TimeSpan.FromSeconds(10), name + " connect timeout");
                Require(!socket.UseCookies, name + " cookies disabled");
                Require(socket.SslOptions.RemoteCertificateValidationCallback is null, name + " platform certificate validation");
            }
        }
        Console.WriteLine("PASS all eight default transport configurations");
        var probe = new BlockingHandler();
        services = new ServiceCollection(); services.AddLogging();
        services.AddApi(c => c.AddApiHttpClients(h => h.BaseAddress = new Uri("http://fixture.invalid/tx/v1"), b => b.ConfigurePrimaryHttpMessageHandler(() => probe)));
        using (var provider = services.BuildServiceProvider())
        {
            bool rejected = false;
            try { await provider.GetRequiredService<ISendApi>().SendEmailAsync("fixture-key", Body()); }
            catch (HttpRequestException) { rejected = true; }
            Require(rejected && probe.Count == 0, "HTTP rejected before primary handler");
        }
        Console.WriteLine("PASS plaintext rejected before transport");
        probe = new BlockingHandler();
        services = new ServiceCollection(); services.AddLogging();
        services.AddApi(c => c.AddApiHttpClients(h => h.BaseAddress = new Uri("https://fixture.invalid/tx/v1"), b => {
            b.ConfigurePrimaryHttpMessageHandler(() => probe);
            b.ConfigureHttpClient(h => h.Timeout = TimeSpan.FromMilliseconds(50));
        }));
        using (var provider = services.BuildServiceProvider())
        {
            bool cancelled = false;
            try { await provider.GetRequiredService<ISendApi>().SendEmailAsync("fixture-key", Body()); }
            catch (OperationCanceledException) { cancelled = true; }
            Require(cancelled && probe.Count == 1, "timeout cancels without SDK replay");
        }
        Console.WriteLine("PASS configurable timeout cancels request");
        return 3;
    }
    sealed class BlockingHandler : HttpMessageHandler
    {
        public int Count;
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Count++;
            await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
