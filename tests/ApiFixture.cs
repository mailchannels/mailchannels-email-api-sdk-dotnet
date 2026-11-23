using System.Net;
using MailChannels.EmailApi.Extensions;
using Microsoft.Extensions.DependencyInjection;

sealed class ApiFixture(int status, string json) : HttpMessageHandler
{
    public string? Method, Body, ContentType, ApiKey, UserAgent;
    public Uri? Uri;
    public System.Text.Json.JsonSerializerOptions Options = null!;
    public T Input<T>(string json) => System.Text.Json.JsonSerializer.Deserialize<T>(json, Options)!;
    public static void Require(bool value, string message) { if (!value) throw new Exception(message); }
    public static async Task Run<T>(string method, string path, int status, string json, Func<T, ApiFixture, Task> check, bool authenticated = true) where T : notnull
    {
        var fixture = new ApiFixture(status, json);
        var services = new ServiceCollection(); services.AddLogging();
        services.AddApi(c => c.AddApiHttpClients(h => h.BaseAddress = new Uri("https://fixture.invalid/tx/v1"), b => b.ConfigurePrimaryHttpMessageHandler(() => fixture)));
        using var provider = services.BuildServiceProvider();
        fixture.Options = provider.GetRequiredService<MailChannels.EmailApi.Client.JsonSerializerOptionsProvider>().Options;
        await check(provider.GetRequiredService<T>(), fixture);
        Require(fixture.Method == method && fixture.Uri!.AbsolutePath == "/tx/v1" + path, "request route " + method + " " + path);
        Require(fixture.ApiKey == (authenticated ? "fixture-key" : null), "authentication presence");
        // Bump with packageVersion in codegen/config.json.
        Require(fixture.UserAgent == "mailchannels-dotnet/0.1.0-alpha.1", "User-Agent " + fixture.UserAgent);
        Console.WriteLine("PASS " + method + " " + path);
    }
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
    {
        Method = request.Method.Method; Uri = request.RequestUri;
        ApiKey = request.Headers.TryGetValues("X-Api-Key", out var values) ? values.Single() : null;
        ContentType = request.Content?.Headers.ContentType?.MediaType;
        UserAgent = request.Headers.UserAgent.ToString();
        Body = request.Content is null ? null : await request.Content.ReadAsStringAsync(token);
        return new HttpResponseMessage((HttpStatusCode)status) { Content = new StringContent(json) };
    }
}
