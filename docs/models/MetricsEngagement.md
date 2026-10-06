# MailChannels.EmailApi.Model.MetricsEngagement

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Buckets** | [**MetricsEngagementBuckets**](MetricsEngagementBuckets.md) |  | 
**Click** | **int** | Count of click events by recipients.  | 
**ClickTrackingDelivered** | **int** | Count of recipients of delivered messages with HTML content that contains tracked click URLs, where click tracking is enabled in the send request.  | 
**Open** | **int** | Count of open events by recipients.  | 
**OpenTrackingDelivered** | **int** | Count of recipients of delivered messages with HTML content where open tracking was enabled in the send request.  | 
**EndTime** | **DateTime** | The end of the time range for retrieving message engagement metrics (exclusive).  | [optional] 
**StartTime** | **DateTime** | The beginning of the time range for retrieving message engagement metrics (inclusive).  | [optional] 
**UniqueClick** | **int** | Count of distinct messages that had at least one click event. Unlike &#x60;click&#x60;, each message is counted at most once regardless of how many links were clicked or how many times. Use this to compute click rates without exceeding 100%.  | [optional] 
**UniqueClickTrackingDelivered** | **int** | Count of distinct messages delivered with click tracking enabled (message-level, not recipient-level). Use as the denominator when computing unique click rates.  | [optional] 
**UniqueOpen** | **int** | Count of distinct messages that had at least one open event. Unlike &#x60;open&#x60;, each message is counted at most once regardless of how many times its tracking pixel was fired. Use this to compute open rates without exceeding 100%.  | [optional] 
**UniqueOpenTrackingDelivered** | **int** | Count of distinct messages delivered with open tracking enabled (message-level, not recipient-level). Use as the denominator when computing unique open rates.  | [optional] 

[[Back to Model list]](../../README.md#documentation-for-models) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to README]](../../README.md)

