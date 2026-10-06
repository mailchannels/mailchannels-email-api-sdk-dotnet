using System.Net;
using System.Text.Json;
using System.Web;
using MailChannels.EmailApi.Api;
using MailChannels.EmailApi.Model;
using static ApiFixture;

static class SuppressionProbe
{
    public static async Task<int> RunAsync()
    {
        await Run<ISuppressionApi>("POST", "/suppression-list", 201, "", async (api,h) => {
            var input = new SuppressionListInput(new() {new("one+tag@example.invalid", notes: new MailChannels.EmailApi.Client.Option<string?>(null), suppressionTypes: new List<SuppressionEntry.SuppressionTypesEnum>{SuppressionEntry.SuppressionTypesEnum.Transactional, SuppressionEntry.SuppressionTypesEnum.NonTransactional})}, addToSubAccounts:true);
            Require((await api.CreateSuppressionsAsync("fixture-key",input)).StatusCode == HttpStatusCode.Created, "create status");
            using var doc = JsonDocument.Parse(h.Body!); var root = doc.RootElement; var entry = root.GetProperty("suppression_entries")[0];
            Require(root.GetProperty("add_to_sub_accounts").GetBoolean() && entry.GetProperty("notes").ValueKind == JsonValueKind.Null && entry.GetProperty("suppression_types")[1].GetString() == "non-transactional", "scope nullable notes and enum serialization");
        });
        await Run<ISuppressionApi>("GET", "/suppression-list", 200, "{\"suppression_list\":[{\"recipient\":\"one+tag@example.invalid\",\"sender\":null,\"notes\":null,\"source\":\"spam_complaint\",\"created_at\":\"2026-10-01T00:00:00Z\",\"suppression_types\":[\"non-transactional\"]}]}", async (api,h) => {
            var r = (await api.ListSuppressionsAsync("fixture-key", "one+tag@example.invalid", "spam_complaint", "2026-10-02", "2026-10-01",5,10)).Ok()!;
            var entry = r.SuppressionList.Single();
            Require(entry.Recipient == "one+tag@example.invalid" && entry.Sender is null && entry.Notes is null && entry.Source == SuppressionEntryResponse.SourceEnum.SpamComplaint && entry.SuppressionTypes!.Single() == SuppressionEntryResponse.SuppressionTypesEnum.NonTransactional, "suppression response");
            var q = HttpUtility.ParseQueryString(h.Uri!.Query);
            Require(q.Count == 6 && q["recipient"] == "one+tag@example.invalid" && q["source"] == "spam_complaint" && q["created_before"] == "2026-10-02" && q["created_after"] == "2026-10-01" && q["limit"] == "5" && q["offset"] == "10", "suppression filters");
        });
        foreach (var all in new[]{false,true})
            await Run<ISuppressionApi>("DELETE", "/suppression-list/recipients/one%2Btag%40example.invalid", 204, "", async (api,h) => {
                var r = all ? await api.DeleteSuppressionAsync("one+tag@example.invalid", "fixture-key", "all") : await api.DeleteSuppressionAsync("one+tag@example.invalid", "fixture-key");
                Require(r.StatusCode == HttpStatusCode.NoContent && h.Body is null && h.Uri!.Query == (all ? "?source=all" : ""), "delete scope");
            });
        return 4;
    }
}
