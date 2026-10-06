using System.Net;
using System.Text.Json;
using System.Web;
using MailChannels.EmailApi.Api;
using MailChannels.EmailApi.Model;
using static ApiFixture;

static class DomainProbe
{
    const string Key = "{\"domain\":\"example.invalid\",\"selector\":\"fixture\",\"public_key\":\"fixture-public-key\",\"status\":\"active\",\"algorithm\":\"rsa\",\"created_at\":null,\"key_length\":2048,\"dkim_dns_records\":[{\"name\":\"fixture._domainkey.example.invalid\",\"type\":\"TXT\",\"value\":\"fixture-dns\"}]}";
    const string Domain = "{\"name\":\"fixture\",\"hostname\":\"click.example.invalid\",\"scope\":\"click\",\"status\":\"active\",\"created_at\":\"2026-10-01T00:00:00Z\"}";
    const string Dns = "{\"instructions\":\"fixture DNS required\",\"token\":\"fixture-token\",\"txt_record_name\":\"_mailchannels-verify.click.example.invalid\",\"txt_record_value\":\"fixture-token\"}";
    static void Body(ApiFixture h, string expected) => Require(System.Text.Json.Nodes.JsonNode.DeepEquals(System.Text.Json.Nodes.JsonNode.Parse(h.Body!), System.Text.Json.Nodes.JsonNode.Parse(expected)), "request payload contract");
    public static async Task<int> RunAsync()
    {
        await Run<IDKIMApi>("POST", "/check-domain", 200, "{\"check_results\":{\"dkim\":[{\"verdict\":\"failed\",\"reason\":\"fixture mismatch\"}]},\"references\":[\"https://example.invalid/help\"]}", async (api,h) => {
            const string input = "{\"domain\":\"example.invalid\",\"envelope_from_domain\":\"bounce.example.invalid\",\"sender_id\":\"fixture\",\"dkim_settings\":[{\"dkim_domain\":\"example.invalid\",\"dkim_selector\":\"fixture\"}]}";
            var r = (await api.CheckDomainAsync("fixture-key", h.Input<CheckDomainBody>(input))).Ok()!;
            var json = JsonSerializer.SerializeToElement(r, h.Options);
            Require(json.GetProperty("check_results").GetProperty("dkim")[0].GetProperty("verdict").GetString() == "failed", "failed domain verdict"); Body(h,input);
        });
        await Run<IDKIMApi>("POST", "/domains/example.invalid/dkim-keys", 201, Key, async (api,h) => {
            const string input = "{\"selector\":\"fixture\",\"algorithm\":\"rsa\",\"key_length\":2048}";
            var r = (await api.CreateDkimKeyAsync("example.invalid","fixture-key",h.Input<DKIMKeyPairCreateRequest>(input))).Created()!;
            var json = JsonSerializer.SerializeToElement(r,h.Options);
            Require(r.Selector == "fixture" && json.GetProperty("dkim_dns_records")[0].GetProperty("type").GetString() == "TXT" && json.GetProperty("created_at").ValueKind == JsonValueKind.Null, "DKIM DNS record"); Body(h,input);
        });
        await Run<IDKIMApi>("GET", "/domains/example.invalid/dkim-keys", 200, "{\"keys\":[]}", async (api,h) => {
            var r = (await api.ListDkimKeysAsync("example.invalid","fixture-key","fixture","active",10,5,true)).Ok()!;
            var q = HttpUtility.ParseQueryString(h.Uri!.Query);
            Require(r.Keys.Count == 0 && q.Count == 5 && q["selector"] == "fixture" && q["status"] == "active" && q["offset"] == "10" && q["limit"] == "5" && q["include_dns_record"] == "true", "DKIM listing filters");
        });
        await Run<IDKIMApi>("POST", "/domains/example.invalid/dkim-keys/old/rotate", 201, "{\"new_key\":{\"domain\":\"example.invalid\",\"selector\":\"new\",\"public_key\":\"new-public\",\"status\":\"active\",\"algorithm\":\"rsa\"},\"rotated_key\":{\"domain\":\"example.invalid\",\"selector\":\"old\",\"public_key\":\"old-public\",\"status\":\"rotated\",\"algorithm\":\"rsa\",\"retiresAt\":null,\"gracePeriodExpiresAt\":null}}", async (api,h) => {
            const string input = "{\"new_key\":{\"selector\":\"new\"}}";
            var r = (await api.RotateDkimKeyAsync("example.invalid","old","fixture-key",h.Input<DKIMKeyRotateRequest>(input))).Created()!;
            Require(r.NewKey.Selector == "new" && r.RotatedKey.Selector == "old", "distinct rotation keys"); Body(h,input);
        });
        await Run<IDKIMApi>("PATCH", "/domains/example.invalid/dkim-keys/fixture", 204, "", async (api,h) => {
            const string input = "{\"status\":\"revoked\"}";
            Require((await api.UpdateDkimKeyAsync("example.invalid","fixture","fixture-key",h.Input<DKIMKeyPairUpdateRequest>(input))).StatusCode == HttpStatusCode.NoContent,"revoke response"); Body(h,input);
        });
        foreach (var status in new[]{201,202})
            await Run<ICustomTrackingApi>("POST", "/custom-tracking-domains", status, status == 201 ? Domain : Dns, async (api,h) => {
                const string input = "{\"name\":\"fixture\",\"hostname\":\"click.example.invalid\",\"scope\":\"click\"}";
                var r = await api.CreateCustomTrackingDomainAsync("fixture-key",h.Input<PostCustomTrackingDomainRequest>(input));
                Require(status == 201 ? r.Created()!.Hostname == "click.example.invalid" && r.Accepted() is null : r.Accepted()!.Token == "fixture-token" && r.Created() is null,"tracking create status model"); Body(h,input);
            });
        await Run<ICustomTrackingApi>("GET", "/custom-tracking-domains", 200, "{\"custom_tracking_domains\":[],\"total\":0}", async (api,h) => {
            var r = (await api.ListCustomTrackingDomainsAsync("fixture-key","fixture","active","click",5,10)).Ok()!;
            var q = HttpUtility.ParseQueryString(h.Uri!.Query);
            Require(r.Total == 0 && r.CustomTrackingDomains.Count == 0 && q.Count == 5 && q["name"] == "fixture" && q["status"] == "active" && q["scope"] == "click" && q["limit"] == "5" && q["offset"] == "10", "tracking listing");
        });
        foreach (var status in new[]{200,202})
            await Run<ICustomTrackingApi>("PATCH", "/custom-tracking-domains/click.example.invalid/click", status, status == 200 ? Domain : Dns, async (api,h) => {
                const string input = "{\"status\":\"active\"}";
                var r = await api.UpdateCustomTrackingDomainAsync("click.example.invalid","click","fixture-key",h.Input<PatchCustomTrackingDomainRequest>(input));
                Require(status == 200 ? r.Ok()!.Hostname == "click.example.invalid" && r.Accepted() is null : r.Accepted()!.Instructions == "fixture DNS required" && r.Ok() is null,"tracking update status model"); Body(h,input);
            });
        await Run<ICustomTrackingApi>("DELETE", "/custom-tracking-domains/click.example.invalid/click", 204, "", async (api,h) => Require((await api.DeleteCustomTrackingDomainAsync("click.example.invalid","click","fixture-key")).StatusCode == HttpStatusCode.NoContent && h.Body is null,"tracking delete"));
        return 11;
    }
}
