# MailChannels.EmailApi.Model.WebhookResponse
The HTTP response returned by the webhook, including status code and response body. A null value indicates no response was received. Possible reasons include timeouts, connection failures, or other network-related issues. 

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Status** | **int** | HTTP status code returned by the webhook  | 
**Body** | **string** | Response body from webhook. Returns an error if unprocessable or too large.  | [optional] 

[[Back to Model list]](../../README.md#documentation-for-models) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to README]](../../README.md)

