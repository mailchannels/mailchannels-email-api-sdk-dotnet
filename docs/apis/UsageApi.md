# MailChannels.EmailApi.Api.UsageApi

All URIs are relative to *https://api.mailchannels.net/tx/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**GetUsage**](UsageApi.md#getusage) | **GET** /usage | Retrieve Usage Stats |

<a id="getusage"></a>
# **GetUsage**
> UsageStats GetUsage (string xApiKey)

Retrieve Usage Stats

Retrieves usage statistics during the current billing period.


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **xApiKey** | **string** |  |  |

### Return type

[**UsageStats**](UsageStats.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successfully returned the usage stats. |  -  |
| **500** | Internal Server Error |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

