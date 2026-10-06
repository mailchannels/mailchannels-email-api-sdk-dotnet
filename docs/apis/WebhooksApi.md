# MailChannels.EmailApi.Api.WebhooksApi

All URIs are relative to *https://api.mailchannels.net/tx/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateWebhook**](WebhooksApi.md#createwebhook) | **POST** /webhook | Enroll for Webhook Notifications |
| [**DeleteWebhooks**](WebhooksApi.md#deletewebhooks) | **DELETE** /webhook | Delete Customer Webhooks |
| [**GetWebhookSigningKey**](WebhooksApi.md#getwebhooksigningkey) | **GET** /webhook/public-key | Retrieve Webhook Signing Key |
| [**ListWebhookBatches**](WebhooksApi.md#listwebhookbatches) | **GET** /webhook-batch | Retrieve Webhook Batches |
| [**ListWebhooks**](WebhooksApi.md#listwebhooks) | **GET** /webhook | Retrieve Customer Webhooks |
| [**ResendWebhookBatch**](WebhooksApi.md#resendwebhookbatch) | **POST** /webhook-batch/{batch_id}/resend | Resend Events |
| [**ValidateWebhook**](WebhooksApi.md#validatewebhook) | **POST** /webhook/validate | Validate Enrolled Webhook |

<a id="createwebhook"></a>
# **CreateWebhook**
> void CreateWebhook (string endpoint, string xApiKey)

Enroll for Webhook Notifications

Enrolls the customer to receive event notifications via webhooks. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **endpoint** | **string** | the URL to which the webhook should be sent |  |
| **xApiKey** | **string** |  |  |

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | Successfully enrolled customer to receive webhooks |  -  |
| **400** | Bad Request |  -  |
| **409** | There&#39;s already a webhook endpoint for this customer |  -  |
| **500** | Internal Server Error |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="deletewebhooks"></a>
# **DeleteWebhooks**
> void DeleteWebhooks (string xApiKey)

Delete Customer Webhooks

Deletes all registered webhook endpoints for the customer. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **xApiKey** | **string** |  |  |

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** | Successfully removed webhook endpoint(s) |  -  |
| **500** | Internal Server Error |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="getwebhooksigningkey"></a>
# **GetWebhookSigningKey**
> Key GetWebhookSigningKey (string id)

Retrieve Webhook Signing Key

Retrieves the public key used to verify signatures on incoming webhook payloads. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | the ID of the key |  |

### Return type

[**Key**](Key.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successfully return the webhook signing key |  -  |
| **404** | The key is not found |  -  |
| **500** | Internal Server Error |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="listwebhookbatches"></a>
# **ListWebhookBatches**
> WebhookBatchResult ListWebhookBatches (string xApiKey, string createdAfter = null, string createdBefore = null, List<string> statuses = null, string webhook = null, int limit = null, int offset = null)

Retrieve Webhook Batches

Retrieves paged webhook batches associated with the customer. The time range specified by created_after and created_before filters must not exceed 31 days. If neither is specified, the default time range is the last 3 days. Optional filters include status categories, webhook, limit and offset. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **xApiKey** | **string** |  |  |
| **createdAfter** | **string** | Inclusive lower bound(UTC) for filtering webhook batches by creation time. Formats: YYYY-MM-DD or YYYY-MM-DDTHH:MM:SSZ  | [optional]  |
| **createdBefore** | **string** | Exclusive upper bound(UTC) for filtering webhook batches by creation time. Formats: YYYY-MM-DD or YYYY-MM-DDTHH:MM:SSZ  | [optional]  |
| **statuses** | [**List&lt;string&gt;**](string.md) | Filters webhook batches by webhook response status category. Values must be unique and encoded as a comma-separated list in the query string. If not provided, batches with all categories are returned.  | [optional]  |
| **webhook** | **string** | Filters webhook batches by the webhook endpoint to which events in the batch were posted.  | [optional]  |
| **limit** | **int** | The maximum number of webhook batches to return  | [optional] [default to 500] |
| **offset** | **int** | The number of webhook batches to skip before starting to collect the result set  | [optional] [default to 0] |

### Return type

[**WebhookBatchResult**](WebhookBatchResult.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successfully returned Webhook batches  |  -  |
| **400** | Bad Request |  -  |
| **500** | Internal Server Error |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="listwebhooks"></a>
# **ListWebhooks**
> List&lt;Webhook&gt; ListWebhooks (string xApiKey)

Retrieve Customer Webhooks

Retrieves all registered webhook endpoints associated with the customer. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **xApiKey** | **string** |  |  |

### Return type

[**List&lt;Webhook&gt;**](Webhook.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successfully returning customer&#39;s webhooks |  -  |
| **500** | Internal Server Error |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="resendwebhookbatch"></a>
# **ResendWebhookBatch**
> WebhookResendResponse ResendWebhookBatch (long batchId, string xApiKey)

Resend Events

Synchronously resend the webhook batch with the provided batch_id for the customer. The result is returned in the response. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **batchId** | **long** | the ID of the batch |  |
| **xApiKey** | **string** |  |  |

### Return type

[**WebhookResendResponse**](WebhookResendResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Resend attempt completed. The result of the resend attempt is included in the response body. A successful response here does not mean the webhook endpoint responded with a 2xx status code, only that we were able to make the resend attempt and receive a response.  |  -  |
| **400** | Bad Request. The batch ID is invalid. |  -  |
| **404** | The batch ID is not found for the customer. |  -  |
| **500** | Internal Server Error |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="validatewebhook"></a>
# **ValidateWebhook**
> WebhookValidationResults ValidateWebhook (string xApiKey, WebhookValidationRequestBody webhookValidationRequestBody = null)

Validate Enrolled Webhook

Validates whether your enrolled webhook(s) respond with an HTTP 2xx status code. Sends a test request to each webhook containing your customer handle, a hardcoded event type(test), a hardcoded sender email(test@mailchannels.com),a timestamp, a request ID (provided or generated), and an SMTP ID. The response includes the HTTP status code and body returned by each webhook. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **xApiKey** | **string** |  |  |
| **webhookValidationRequestBody** | [**WebhookValidationRequestBody**](WebhookValidationRequestBody.md) |  | [optional]  |

### Return type

[**WebhookValidationResults**](WebhookValidationResults.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Webhook validation completed  |  -  |
| **400** | Bad Request. Provided request ID is too long. |  -  |
| **404** | No webhooks found for the account |  -  |
| **500** | Internal Server Error |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

