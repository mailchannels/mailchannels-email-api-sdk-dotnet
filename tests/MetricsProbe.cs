using System.Web;
using MailChannels.EmailApi.Api;
using static ApiFixture;

static class MetricsProbe
{
    static void Filters(ApiFixture h) {
        var q = HttpUtility.ParseQueryString(h.Uri!.Query);
        Require(q.Count == 4 && q["start_time"] == "2026-10-01T00:00:00Z" && q["end_time"] == "2026-10-02T00:00:00Z" && q["campaign_id"] == "fixture+campaign" && q["interval"] == "day", "metrics filters");
    }
    public static async Task<int> RunAsync()
    {
        await Run<IMetricsApi>("GET", "/metrics/engagement", 200, "{\"open\":3,\"open_tracking_delivered\":4,\"click\":2,\"click_tracking_delivered\":4,\"unique_open\":2,\"unique_click\":1,\"buckets\":{\"open\":[{\"period_start\":\"2026-10-01T00:00:00Z\",\"count\":3}],\"open_tracking_delivered\":[],\"click\":[],\"click_tracking_delivered\":[]}}", async (api,h) => {
            var result = (await api.GetEngagementMetricsAsync("fixture-key", "2026-10-01T00:00:00Z", "2026-10-02T00:00:00Z", "fixture+campaign", "day")).Ok()!;
            Require(result.UniqueClick == 1 && result.Buckets.Open[0].Count == 3, "typed metrics and buckets"); Filters(h);
        });
        await Run<IMetricsApi>("GET", "/metrics/performance", 200, "{\"delivered\":10,\"bounced\":2,\"complained\":1,\"processed\":12,\"buckets\":{\"delivered\":[],\"bounced\":[{\"period_start\":\"2026-10-01T00:00:00Z\",\"count\":2}],\"complained\":[],\"processed\":[]}}", async (api,h) => {
            var result = (await api.GetPerformanceMetricsAsync("fixture-key", "2026-10-01T00:00:00Z", "2026-10-02T00:00:00Z", "fixture+campaign", "day")).Ok()!;
            Require(result.Bounced == 2 && result.Buckets.Bounced[0].Count == 2, "typed metrics and buckets"); Filters(h);
        });
        await Run<IMetricsApi>("GET", "/metrics/recipient-behaviour", 200, "{\"unsubscribed\":2,\"unsubscribe_delivered\":10,\"buckets\":{\"unsubscribed\":[{\"period_start\":\"2026-10-01T00:00:00Z\",\"count\":2}],\"unsubscribe_delivered\":[]}}", async (api,h) => {
            var result = (await api.GetRecipientBehaviourMetricsAsync("fixture-key", "2026-10-01T00:00:00Z", "2026-10-02T00:00:00Z", "fixture+campaign", "day")).Ok()!;
            Require(result.Unsubscribed == 2 && result.Buckets.Unsubscribed[0].Count == 2, "typed metrics and buckets"); Filters(h);
        });
        await Run<IMetricsApi>("GET", "/metrics/volume", 200, "{\"processed\":12,\"delivered\":10,\"dropped\":2,\"buckets\":{\"processed\":[],\"delivered\":[],\"dropped\":[{\"period_start\":\"2026-10-01T00:00:00Z\",\"count\":2}]}}", async (api,h) => {
            var result = (await api.GetVolumeMetricsAsync("fixture-key", "2026-10-01T00:00:00Z", "2026-10-02T00:00:00Z", "fixture+campaign", "day")).Ok()!;
            Require(result.Dropped == 2 && result.Buckets.Dropped[0].Count == 2, "typed metrics and buckets"); Filters(h);
        });
        await Run<IMetricsApi>("GET", "/metrics/senders/campaigns", 200, "{\"limit\":5,\"offset\":10,\"total\":20,\"senders\":[{\"name\":\"fixture-campaign\",\"processed\":10,\"delivered\":8,\"bounced\":1,\"dropped\":1}]}", async (api,h) => {
            var r = (await api.GetSenderMetricsAsync("campaigns", "fixture-key", limit:5, offset:10, sortOrder:"desc")).Ok()!;
            Require(r.Total == 20 && r.Limit == 5 && r.Offset == 10 && r.Senders[0].Delivered == 8, "sender metrics");
            var q = HttpUtility.ParseQueryString(h.Uri!.Query); Require(q.Count == 3 && q["limit"] == "5" && q["offset"] == "10" && q["sort_order"] == "desc", "sender filters");
        });
        await Run<IUsageApi>("GET", "/usage", 200, "{\"monthly_limit\":0,\"total_usage\":4294967296,\"period_start_date\":\"2026-10-01\",\"period_end_date\":\"2026-10-31\"}", async (api,h) => {
            var r = (await api.GetUsageAsync("fixture-key")).Ok()!;
            Require(r.MonthlyLimit == 0 && r.TotalUsage == 4294967296 && r.PeriodStartDate == new DateOnly(2026,10,1), "parent usage");
        });
        return 6;
    }
}
