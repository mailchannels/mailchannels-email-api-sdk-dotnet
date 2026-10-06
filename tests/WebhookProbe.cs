using System.Net;
using System.Text.Json;
using System.Web;
using MailChannels.EmailApi.Api;
using MailChannels.EmailApi.Model;
using static ApiFixture;

static class WebhookProbe
{
    public static async Task<int> RunAsync()
    {
        const string endpoint = "https://example.invalid/hook?a=1&token=fixture+value#frag";
        await Run<IWebhooksApi>("POST", "/webhook", 201, "", async (api,h) => {
            var r = await api.CreateWebhookAsync(endpoint, "fixture-key");
            var query = HttpUtility.ParseQueryString(h.Uri!.Query);
            Require(r.StatusCode == HttpStatusCode.Created && query.Count == 1 && query["endpoint"] == endpoint && h.Body is null, "endpoint query encoding");
        });
        await Run<IWebhooksApi>("GET", "/webhook", 200, "[{\"webhook\":\"https://example.invalid/a\"},{\"webhook\":\"https://example.invalid/b\"}]", async (api,h) => {
            var list = (await api.ListWebhooksAsync("fixture-key")).Ok()!;
            Require(list.Count == 2 && list[1].VarWebhook == "https://example.invalid/b", "webhook list");
        });
        await Run<IWebhooksApi>("DELETE", "/webhook", 204, "", async (api,h) => Require((await api.DeleteWebhooksAsync("fixture-key")).StatusCode == HttpStatusCode.NoContent && h.Body is null, "delete webhooks"));
        await Run<IWebhooksApi>("GET", "/webhook/public-key", 200, "{\"id\":\"fixture+key\",\"key\":\"fixture-public-key\"}", async (api,h) => {
            var key = (await api.GetWebhookSigningKeyAsync("fixture+key")).Ok()!;
            Require(key.Id == "fixture+key" && key.VarKey == "fixture-public-key", "signing key response");
            Require(HttpUtility.ParseQueryString(h.Uri!.Query)["id"] == "fixture+key", "key id query");
        }, authenticated:false);
        await Run<IWebhooksApi>("GET", "/webhook-batch", 200, "{\"webhook_batches\":[{\"batch_id\":4294967296,\"customer_handle\":\"fixture\",\"webhook\":\"https://example.invalid/hook\",\"status\":\"no_response\",\"status_code\":null,\"created_at\":\"2026-10-01T00:00:00Z\",\"event_count\":2}]}", async (api,h) => {
            var result = (await api.ListWebhookBatchesAsync("fixture-key", "2026-10-01", "2026-10-02", new List<string>{"no_response","5xx"}, endpoint, 10,20)).Ok()!;
            Require(result.WebhookBatches[0].BatchId == 4294967296 && result.WebhookBatches[0].StatusCode is null && result.WebhookBatches[0].EventCount == 2, "batch data");
            var query = HttpUtility.ParseQueryString(h.Uri!.Query);
            Require(query.Count == 6 && query["statuses"] == "no_response,5xx" && query["webhook"] == endpoint && query["limit"] == "10" && query["offset"] == "20" && query["created_after"] == "2026-10-01" && query["created_before"] == "2026-10-02", "batch filters");
        });
        await Run<IWebhooksApi>("POST", "/webhook-batch/4294967296/resend", 200, "{\"batch_id\":4294967296,\"customer_handle\":\"fixture\",\"webhook\":\"https://example.invalid/hook\",\"created_at\":\"2026-10-01T00:00:00Z\",\"event_count\":2,\"status_code\":null,\"duration_in_ms\":null}", async (api,h) => {
            var receipt = (await api.ResendWebhookBatchAsync(4294967296, "fixture-key")).Ok()!;
            Require(receipt.BatchId == 4294967296 && receipt.StatusCode is null && receipt.DurationInMs is null, "resend receipt");
        });
        await Run<IWebhooksApi>("POST", "/webhook/validate", 200, "{\"all_passed\":false,\"results\":[]}", async (api,h) => {
            var r = (await api.ValidateWebhookAsync("fixture-key", new WebhookValidationRequestBody(requestId:"fixture-request"))).Ok()!;
            Require(!r.AllPassed && r.Results.Count == 0, "validation failure retained");
            using var body = JsonDocument.Parse(h.Body!); Require(body.RootElement.GetProperty("request_id").GetString() == "fixture-request", "validation request id");
        });
        return 7;
    }
}
