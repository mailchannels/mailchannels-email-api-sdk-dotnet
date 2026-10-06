using System.Net;
using System.Text.Json;
using MailChannels.EmailApi.Api;
using MailChannels.EmailApi.Extensions;
using MailChannels.EmailApi.Model;
using Microsoft.Extensions.DependencyInjection;

static class SubaccountProbe
{
    static void Check(bool value, string message) { if (!value) throw new Exception(message); }
    public static async Task<int> RunAsync()
    {
        int passed = 0;
        async Task Run(string method, string path, int status, string json, Func<ISubAccountsApi, Capture, Task> call)
        {
            var capture = new Capture(status, json);
            var services = new ServiceCollection(); services.AddLogging();
            services.AddApi(c => c.AddApiHttpClients(h => h.BaseAddress = new Uri("https://fixture.invalid/tx/v1"), b => b.ConfigurePrimaryHttpMessageHandler(() => capture)));
            using var provider = services.BuildServiceProvider();
            await call(provider.GetRequiredService<ISubAccountsApi>(), capture);
            Check(capture.Method == method && capture.Path == "/tx/v1" + path, "route: " + method + " " + path + " actual " + capture.Path);
            Check(capture.Key == "fixture-key", "authentication");
            passed++; Console.WriteLine("PASS subaccount " + method + " " + path);
        }
        await Run("GET", "/sub-account/team%2Fa%20b%2B%3F%23/limit", 200, "{\"sends\":-1}", async (api,h) => {
            var r = await api.GetSubaccountLimitAsync("team/a b+?#", "fixture-key"); Check(r.Ok()!.Sends == -1, "inherited limit");
        });
        await Run("PUT", "/sub-account/fixture/limit", 200, "{\"limit\":{\"sends\":0}}", async (api,h) => {
            var r = await api.SetSubaccountLimitAsync("fixture", "fixture-key", new LimitInput(0));
            Check(r.Ok()!.Limit?.Sends == 0, "zero limit retained");
            using var json = JsonDocument.Parse(h.Body!); Check(json.RootElement.GetProperty("sends").GetInt32() == 0, "zero serialized");
        });
        await Run("DELETE", "/sub-account/fixture/limit", 204, "", async (api,h) => {
            var r = await api.DeleteSubaccountLimitAsync("fixture", "fixture-key"); Check(r.StatusCode == HttpStatusCode.NoContent && h.Body is null, "empty delete");
        });
        await Run("POST", "/sub-account/fixture/api-key", 201, "{\"id\":41,\"key\":\"fixture-created-secret\"}", async (api,h) => {
            var key = (await api.CreateSubaccountApiKeyAsync("fixture", "fixture-key")).Created()!;
            Check(key.Id == 41 && key.Key == "fixture-created-secret" && !key.ToString().Contains("fixture-created-secret"), "created key");
        });
        await Run("GET", "/sub-account/fixture/api-key?limit=10&offset=20", 200, "[{\"id\":41}]", async (api,h) => {
            var keys = (await api.ListSubaccountApiKeysAsync("fixture", "fixture-key", 10, 20)).Ok()!; Check(keys.Count == 1 && keys[0].Id == 41 && keys[0].Key is null, "listed keys");
        });
        await Run("DELETE", "/sub-account/fixture/api-key/41", 204, "", async (api,h) => Check((await api.DeleteSubaccountApiKeyAsync("fixture", 41, "fixture-key")).StatusCode == HttpStatusCode.NoContent, "delete key"));
        await Run("POST", "/sub-account", 201, "{\"handle\":\"fixture\",\"enabled\":true,\"company_name\":\"Fixture Company\"}", async (api,h) => {
            var r = (await api.CreateSubaccountAsync("fixture-key", new SubAccountData("Fixture Company", "fixture"))).Created()!;
            Check(r.Handle == "fixture" && r.Enabled, "created account");
            using var json = JsonDocument.Parse(h.Body!); Check(json.RootElement.GetProperty("company_name").GetString() == "Fixture Company", "company field");
        });
        await Run("POST", "/sub-account", 201, "{\"handle\":\"generated\",\"enabled\":true}", async (api,h) => {
            var r = await api.CreateSubaccountAsync("fixture-key"); Check(r.Created()!.Handle == "generated" && h.Body is null && h.ContentType is null, "omitted body");
        });
        await Run("GET", "/sub-account?limit=5&offset=10", 200, "[{\"handle\":\"fixture\",\"enabled\":false}]", async (api,h) => {
            var list = (await api.ListSubaccountsAsync("fixture-key", 5, 10)).Ok()!; Check(list.Count == 1 && !list[0].Enabled, "disabled account");
        });
        await Run("POST", "/sub-account/fixture/activate", 204, "", async (api,h) => Check((await api.ActivateSubaccountAsync("fixture", "fixture-key")).StatusCode == HttpStatusCode.NoContent, "activate"));
        await Run("POST", "/sub-account/fixture/suspend", 204, "", async (api,h) => Check((await api.SuspendSubaccountAsync("fixture", "fixture-key")).StatusCode == HttpStatusCode.NoContent, "suspend"));
        await Run("DELETE", "/sub-account/fixture", 204, "", async (api,h) => Check((await api.DeleteSubaccountAsync("fixture", "fixture-key")).StatusCode == HttpStatusCode.NoContent, "delete account"));
        await Run("GET", "/sub-account/fixture/usage", 200, "{\"monthly_limit\":100,\"total_usage\":4294967296,\"period_start_date\":\"2026-10-01\",\"period_end_date\":\"2026-10-31\"}", async (api,h) => {
            var usage = (await api.GetSubaccountUsageAsync("fixture", "fixture-key")).Ok()!;
            Check(usage.TotalUsage == 4294967296 && usage.PeriodStartDate == new DateOnly(2026,10,1) && usage.PeriodEndDate == new DateOnly(2026,10,31), "usage counts/dates");
        });
        await Run("POST", "/sub-account/fixture/smtp-password", 201, "{\"id\":12,\"enabled\":true,\"smtp_password\":\"fixture-smtp-secret\"}", async (api,h) => {
            var password = (await api.CreateSubaccountSmtpPasswordAsync("fixture", "fixture-key")).Created()!;
            Check(password.Id == 12 && password.Enabled == true && password.SmtpPassword == "fixture-smtp-secret" && !password.ToString().Contains("fixture-smtp-secret"), "smtp secret");
        });
        await Run("GET", "/sub-account/fixture/smtp-password", 200, "[{\"id\":12,\"enabled\":false}]", async (api,h) => {
            var list = (await api.ListSubaccountSmtpPasswordsAsync("fixture", "fixture-key")).Ok()!; Check(list.Count == 1 && list[0].Enabled == false && list[0].SmtpPassword is null, "smtp list");
        });
        await Run("DELETE", "/sub-account/fixture/smtp-password/12", 204, "", async (api,h) => Check((await api.DeleteSubaccountSmtpPasswordAsync("fixture",12,"fixture-key")).StatusCode == HttpStatusCode.NoContent, "smtp delete"));
        return passed;
    }
    sealed class Capture(int status, string json) : HttpMessageHandler
    {
        public string? Method, Path, Key, Body, ContentType;
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage r, CancellationToken ct)
        {
            Method = r.Method.Method; Path = r.RequestUri!.PathAndQuery; Key = r.Headers.GetValues("X-Api-Key").Single();
            Body = r.Content is null ? null : await r.Content.ReadAsStringAsync(ct); ContentType = r.Content?.Headers.ContentType?.MediaType;
            return new HttpResponseMessage((HttpStatusCode)status) {Content = new StringContent(json)};
        }
    }
}
