# MailChannels.EmailApi.Api.SendApi

All URIs are relative to *https://api.mailchannels.net/tx/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**QueueEmail**](SendApi.md#queueemail) | **POST** /send-async | Send an Email Asynchronously |
| [**SendEmail**](SendApi.md#sendemail) | **POST** /send | Send an Email |

<a id="queueemail"></a>
# **QueueEmail**
> AsyncSendResponse QueueEmail (string xApiKey, MailSendBody mailSendBody)

Send an Email Asynchronously

Queues an email message for asynchronous processing and returns immediately with a request ID.  The email will be processed in the background, and you'll receive webhook events for all delivery status updates (e.g. dropped, processed, delivered, hard-bounced). These webhook events are identical to those sent for the synchronous /send endpoint.  Use this endpoint when you need to send emails without waiting for processing to complete. This can improve your application's response time, especially when sending to multiple recipients. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **xApiKey** | **string** |  |  |
| **mailSendBody** | [**MailSendBody**](MailSendBody.md) |  |  |

### Return type

[**AsyncSendResponse**](AsyncSendResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **202** | Request accepted and queued for processing |  -  |
| **400** | Bad Request |  -  |
| **403** | User does not have access to this feature |  -  |
| **413** | Payload too large - The total message size should not exceed 30MB. This includes the message itself, headers, and the combined size of any attachments.  |  -  |
| **500** | Internal Server Error |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="sendemail"></a>
# **SendEmail**
> Message SendEmail (string xApiKey, MailSendBody mailSendBody, bool dryRun = null)

Send an Email

Sends an email message to one or more recipients.  **Click Tracking Notes:** Only links (`<a>` tags) meeting all of the following conditions are processed for click tracking: - The URL is non-empty. - The URL starts with \"http\" or \"https\". - The link does not have a clicktracking attribute set to 'off'. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **xApiKey** | **string** |  |  |
| **mailSendBody** | [**MailSendBody**](MailSendBody.md) |  |  |
| **dryRun** | **bool** | When present and set to true, the message will not be sent. Instead, the fully rendered message is returned. This can be useful for testing.  | [optional]  |

### Return type

[**Message**](Message.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Success. Returned if dry-run is present in the query |  -  |
| **202** | Success |  -  |
| **400** | Bad Request |  -  |
| **403** | User does not have access to this feature |  -  |
| **413** | Payload too large - The total message size should not exceed 30MB. This includes the message itself, headers, and the combined size of any attachments.  |  -  |
| **500** | Internal Server Error |  -  |
| **502** | Bad Gateway |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

