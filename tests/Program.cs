using System.Net;
using System.Text.Json;
using MailChannels.EmailApi.Api;
using MailChannels.EmailApi.Client;
using MailChannels.EmailApi.Extensions;
using MailChannels.EmailApi.Model;
using Microsoft.Extensions.DependencyInjection;

var passed = 0;
void Check(bool condition, string description) { if (!condition) throw new Exception(description); }
MailSendBody Body() => new(new() {new("text/plain", "fixture body")}, new("sender@example.invalid"), new() {new(new() {new("recipient@example.invalid")})}, "fixture subject");
async Task Run(string name, int status, string json, Func<ISendApi, FixtureHandler, Task> check) {
    var handler = new FixtureHandler(status, json);
    var services = new ServiceCollection();
    services.AddLogging();
    services.AddApi(config => config.AddApiHttpClients(c => c.BaseAddress = new Uri("https://fixture.invalid/tx/v1"), b => b.ConfigurePrimaryHttpMessageHandler(() => handler)));
    using var provider = services.BuildServiceProvider();
    await check(provider.GetRequiredService<ISendApi>(), handler);
    passed++;
    Console.WriteLine($"PASS {name}");
}
await Run("dry-run request and 200 response", 200, "{\"data\":[\"valid\"]}", async (api,h) => {
    var response = await api.SendEmailAsync("fixture-key", Body(), true);
    Check(response.IsOk && !response.IsAccepted && response.Ok()!.Data![0] == "valid", "200 typed result");
    Check(response.Accepted() is null, "wrong status accessor");
    Check(h.Uri == "https://fixture.invalid/tx/v1/send?dry-run=true", "dry-run URL");
    Check(h.Method == "POST" && h.Key == "fixture-key" && h.ContentType == "application/json", "request method/auth/content type");
    using var body = JsonDocument.Parse(h.Body!);
    Check(body.RootElement.GetProperty("from").GetProperty("email").GetString() == "sender@example.invalid", "sender serialization");
    Check(!body.RootElement.TryGetProperty("dkim_private_key", out _), "absent optional field");
});
await Run("202 preserves partial results", 202, "{\"request_id\":\"req-123\",\"results\":[{\"index\":0,\"status\":\"sent\",\"message_id\":\"msg-1\"},{\"index\":1,\"status\":\"failed\",\"reason\":\"fixture rejection\"}]}", async (api,h) => {
    var r = await api.SendEmailAsync("fixture-key", Body());
    var result = r.Accepted()!;
    Check(r.IsAccepted && r.Ok() is null && result.RequestId == "req-123", "202 status/request id");
    Check(result.Results!.Count == 2 && result.Results[0].MessageId == "msg-1" && result.Results[1].Reason == "fixture rejection", "partial results");
    Check(h.Uri!.EndsWith("/send"), "unset dry-run omitted");
});
await Run("async receipt", 202, "{\"request_id\":\"queued-123\",\"queued_at\":\"2026-10-06T00:00:00Z\"}", async (api,h) => {
    var r = await api.QueueEmailAsync("fixture-key", Body());
    Check(r.Accepted()!.RequestId == "queued-123" && h.Uri!.EndsWith("/send-async"), "async request/receipt");
});
await Run("400 does not become success", 400, "{\"errors\":[\"fixture bad request\"]}", async (api,h) => {
    var r = await api.SendEmailAsync("fixture-key", Body());
    Check(!r.IsSuccessStatusCode && r.IsBadRequest && r.Accepted() is null && r.Ok() is null, "error status");
    Check(r.RawContent.Contains("fixture bad request"), "error retained");
});
await Run("malformed success is observable", 202, "{invalid", async (api,h) => {
    var r = await api.SendEmailAsync("fixture-key", Body());
    bool threw = false;
    try { r.Accepted(); } catch (JsonException) { threw = true; }
    Check(threw && !r.TryAccepted(out _), "malformed JSON");
});
await Run("503 is not retried", 503, "temporary fixture failure", async (api,h) => {
    var r = await api.SendEmailAsync("fixture-key", Body());
    Check(!r.IsSuccessStatusCode && h.Count == 1, "no replay");
});
await Run("explicit optional enum serialization", 200, "{\"data\":[]}", async (api,h) => {
    var body = Body(); body.Content[0].TemplateType = ContentItem.TemplateTypeEnum.Mustache;
    await api.SendEmailAsync("fixture-key", body, true);
    using var json = JsonDocument.Parse(h.Body!);
    Check(json.RootElement.GetProperty("content")[0].GetProperty("template_type").GetString() == "mustache", "explicit template type");
});
await Run("explicit null enum rejected before transport", 200, "{}", async (api,h) => {
    var body = Body(); body.Content[0].TemplateType = null;
    bool threw = false;
    try { await api.SendEmailAsync("fixture-key", body); } catch (JsonException) { threw = true; }
    Check(threw && h.Count == 0, "non-nullable enum rejects explicit null");
});
passed += await TransportProbe.RunAsync();
passed += await TlsProbe.RunAsync();
passed += await DiagnosticsProbe.RunAsync();
passed += await SubaccountProbe.RunAsync();
passed += await WebhookProbe.RunAsync();
passed += await SuppressionProbe.RunAsync();
passed += await MetricsProbe.RunAsync();
passed += await DomainProbe.RunAsync();
Console.WriteLine($"{passed} native checks passed; handler fixtures and loopback TLS only, no provider requests.");

sealed class FixtureHandler(int status, string json) : HttpMessageHandler {
    public string? Uri, Method, Key, ContentType, Body;
    public int Count;
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token) {
        Count++;
        Uri = request.RequestUri!.ToString(); Method = request.Method.Method;
        Key = request.Headers.GetValues("X-Api-Key").Single();
        ContentType = request.Content?.Headers.ContentType?.MediaType;
        Body = request.Content is null ? null : await request.Content.ReadAsStringAsync(token);
        return new HttpResponseMessage((HttpStatusCode)status) { Content = new StringContent(json), RequestMessage = request };
    }
}
