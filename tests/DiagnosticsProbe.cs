using System.Net;
using System.Text.Json;
using MailChannels.EmailApi.Api;
using MailChannels.EmailApi.Client;
using MailChannels.EmailApi.Extensions;
using MailChannels.EmailApi.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

static class DiagnosticsProbe
{
    const string Secret = "fixture-sensitive-canary";
    static void Require(bool value, string message) { if (!value) throw new Exception(message); }
    public static async Task<int> RunAsync()
    {
        var body = new MailSendBody(new() {new("text/plain", Secret)}, new(Secret + "@example.invalid"), new() {new(new() {new("b@example.invalid")})}, Secret, dkimPrivateKey: Secret);
        Require(!body.ToString().Contains(Secret), "MailSendBody ToString leaks content or DKIM key");
        Require(!body.From.ToString().Contains(Secret), "EmailAddress ToString leaks address");
        Require(!body.Content[0].ToString().Contains(Secret), "ContentItem ToString leaks body");
        Console.WriteLine("PASS model formatting redacts sensitive fields");
        var services = new ServiceCollection(); services.AddLogging(); services.AddApi();
        using (var provider = services.BuildServiceProvider())
        {
            var options = provider.GetRequiredService<JsonSerializerOptionsProvider>().Options;
            Require(JsonSerializer.Serialize(body, options).Contains(Secret), "explicit serialization still retains payload");
            var key = JsonSerializer.Deserialize<APIKey>("{\"id\":1,\"key\":\"" + Secret + "\"}", options);
            Require(key is not null && !key.ToString().Contains(Secret), "API key model formatting");
        }
        Console.WriteLine("PASS key formatting redacted and explicit serialization preserved");
        foreach (var throwing in new[] {true, false})
        {
            var logs = new CaptureLogger();
            services = new ServiceCollection(); services.AddLogging(b => b.AddProvider(logs));
            services.AddApi(c => c.AddApiHttpClients(h => h.BaseAddress = new Uri("https://fixture.invalid/tx/v1"), b => b.ConfigurePrimaryHttpMessageHandler(() => new Handler(throwing))));
            using var provider = services.BuildServiceProvider();
            try {
                var response = await provider.GetRequiredService<ISendApi>().SendEmailAsync(Secret, body);
                Require(!response.TryAccepted(out _), "malformed enum should not deserialize");
            } catch (HttpRequestException) when (throwing) { }
            Require(logs.Text.Count > 0 && !string.Join("\n", logs.Text).Contains(Secret), "SDK error log leaks raw exception or data");
            Console.WriteLine(throwing ? "PASS transport logs redact exception details" : "PASS deserialization logs redact exception details");
        }
        return 4;
    }
    sealed class Handler(bool throwing) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (throwing) throw new HttpRequestException(Secret);
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.Accepted) {Content = new StringContent("{\"results\":[{\"index\":0,\"status\":\"" + Secret + "\"}]}")});
        }
    }
    sealed class CaptureLogger : ILoggerProvider, ILogger
    {
        public List<string> Text = new();
        public ILogger CreateLogger(string name) => this;
        public void Dispose() { }
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;
        public bool IsEnabled(LogLevel level) => true;
        public void Log<TState>(LogLevel level, EventId id, TState state, Exception? error, Func<TState, Exception?, string> formatter) => Text.Add(formatter(state, error) + error?.ToString());
    }
}
