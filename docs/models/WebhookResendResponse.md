# MailChannels.EmailApi.Model.WebhookResendResponse

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**BatchId** | **long** | Unique identifier for the webhook batch  | 
**CreatedAt** | **DateTime** | Timestamp of when the webhook batch was created | 
**CustomerHandle** | **string** | Customer handle associated with the webhook batch | 
**EventCount** | **int** | Number of events in the webhook batch | 
**Webhook** | **string** | Webhook URL to which events in the batch were posted | 
**DurationInMs** | **int** | Duration of the webhook batch in milliseconds, measured from the time the request was sent to the webhook endpoint until the response was received. Null indicates that no response was returned from the webhook endpoint.  | [optional] 
**StatusCode** | **int** | HTTP status code returned by the webhook endpoint. Valid values are 100-599. Null indicates that no response was returned from the webhook endpoint.  | [optional] 

[[Back to Model list]](../../README.md#documentation-for-models) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to README]](../../README.md)

