# MailChannels.EmailApi.Model.WebhookBatch

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**BatchId** | **long** | Unique identifier for the webhook batch  | 
**CreatedAt** | **DateTime** | Timestamp of when the webhook batch was created  | 
**CustomerHandle** | **string** | Customer handle associated with the webhook batch  | 
**EventCount** | **int** | Number of events in the webhook batch  | 
**Status** | **string** | Status of the webhook batch. no_response: no response returned from the webhook endpoint.  | 
**Webhook** | **string** | Webhook endpoint to which events in the batch were posted | 
**Duration** | [**WebhookBatchDuration**](WebhookBatchDuration.md) |  | [optional] 
**StatusCode** | **int** | HTTP status code returned by the webhook endpoint  | [optional] 

[[Back to Model list]](../../README.md#documentation-for-models) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to README]](../../README.md)

