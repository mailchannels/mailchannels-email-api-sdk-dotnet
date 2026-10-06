# MailChannels.EmailApi.Model.MetricsRecipientBehaviour

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Buckets** | [**MetricsRecipientBehaviourBuckets**](MetricsRecipientBehaviourBuckets.md) |  | 
**UnsubscribeDelivered** | **int** | Count of recipients of delivered messages that include at least one of the unsubscribe link or unsubscribe headers. Since the unsubscribe feature requires exactly one recipient per message, this count also represents the total number of delivered messages.  | 
**Unsubscribed** | **int** | Count of unsubscribed events by recipients.  | 
**EndTime** | **DateTime** | The end of the time range for retrieving recipient behaviour metrics (exclusive).  | [optional] 
**StartTime** | **DateTime** | The beginning of the time range for retrieving recipient behaviour metrics (inclusive).  | [optional] 

[[Back to Model list]](../../README.md#documentation-for-models) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to README]](../../README.md)

