# MailChannels.EmailApi.Api.MetricsApi

All URIs are relative to *https://api.mailchannels.net/tx/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**GetEngagementMetrics**](MetricsApi.md#getengagementmetrics) | **GET** /metrics/engagement | Retrieve Engagement Metrics |
| [**GetPerformanceMetrics**](MetricsApi.md#getperformancemetrics) | **GET** /metrics/performance | Retrieve Performance Metrics |
| [**GetRecipientBehaviourMetrics**](MetricsApi.md#getrecipientbehaviourmetrics) | **GET** /metrics/recipient-behaviour | Retrieve Recipient Behaviour Metrics |
| [**GetSenderMetrics**](MetricsApi.md#getsendermetrics) | **GET** /metrics/senders/{sender_type} | Retrieve Sender Metrics |
| [**GetVolumeMetrics**](MetricsApi.md#getvolumemetrics) | **GET** /metrics/volume | Retrieve Volume Metrics |

<a id="getengagementmetrics"></a>
# **GetEngagementMetrics**
> MetricsEngagement GetEngagementMetrics (string xApiKey, string startTime = null, string endTime = null, string campaignId = null, string interval = null)

Retrieve Engagement Metrics

Retrieve engagement metrics for messages sent from your account, including counts of open and click events. Supports optional filters for time range, and campaign ID. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **xApiKey** | **string** |  |  |
| **startTime** | **string** | The beginning of the time range for retrieving message engagement metrics (inclusive). Formats: YYYY-MM-DD or YYYY-MM-DDTHH:MM:SSZ. Defaults to one month ago if not provided.  | [optional]  |
| **endTime** | **string** | The end of the time range for retrieving message engagement metrics (exclusive). Formats: YYYY-MM-DD or YYYY-MM-DDTHH:MM:SSZ. Defaults to the current time if not provided.  | [optional]  |
| **campaignId** | **string** | The ID of the campaign to filter metrics by. If not provided, metrics for all campaigns will be returned.  | [optional]  |
| **interval** | **string** | The interval for aggregating metrics data. Allowed values:   - hour: Hourly breakdown   - day: Daily breakdown (default)   - week: Weekly breakdown   - month: Monthly breakdown  | [optional] [default to day] |

### Return type

[**MetricsEngagement**](MetricsEngagement.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successfully retrieved engagement metrics  |  -  |
| **400** | Bad Request |  -  |
| **500** | Internal Server Error |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="getperformancemetrics"></a>
# **GetPerformanceMetrics**
> MetricsPerformance GetPerformanceMetrics (string xApiKey, string startTime = null, string endTime = null, string campaignId = null, string interval = null)

Retrieve Performance Metrics

Retrieve performance metrics for messages sent from your account, including counts of processed, delivered, hard-bounced, and complained events. Supports optional filters for time range, and campaign ID. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **xApiKey** | **string** |  |  |
| **startTime** | **string** | The beginning of the time range for retrieving message performance metrics (inclusive). Formats: YYYY-MM-DD or YYYY-MM-DDTHH:MM:SSZ. Defaults to one month ago if not provided.  | [optional]  |
| **endTime** | **string** | The end of the time range for retrieving message performance metrics (exclusive). Formats: YYYY-MM-DD or YYYY-MM-DDTHH:MM:SSZ. Defaults to the current time if not provided.  | [optional]  |
| **campaignId** | **string** | The ID of the campaign to filter metrics by. If not provided, metrics for all campaigns will be returned.  | [optional]  |
| **interval** | **string** | The interval for aggregating metrics data. Allowed values:   - hour: Hourly breakdown   - day: Daily breakdown (default)   - week: Weekly breakdown   - month: Monthly breakdown  | [optional] [default to day] |

### Return type

[**MetricsPerformance**](MetricsPerformance.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successfully retrieved performance metrics  |  -  |
| **400** | Bad Request |  -  |
| **500** | Internal Server Error |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="getrecipientbehaviourmetrics"></a>
# **GetRecipientBehaviourMetrics**
> MetricsRecipientBehaviour GetRecipientBehaviourMetrics (string xApiKey, string startTime = null, string endTime = null, string campaignId = null, string interval = null)

Retrieve Recipient Behaviour Metrics

Retrieve recipient behaviour metrics for messages sent from your account, including counts of unsubscribed events. Supports optional filters for time range, and campaign ID. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **xApiKey** | **string** |  |  |
| **startTime** | **string** | The beginning of the time range for retrieving recipient behaviour metrics (inclusive). Formats: YYYY-MM-DD or YYYY-MM-DDTHH:MM:SSZ. Defaults to one month ago if not provided.  | [optional]  |
| **endTime** | **string** | The end of the time range for retrieving recipient behaviour metrics (exclusive). Formats: YYYY-MM-DD or YYYY-MM-DDTHH:MM:SSZ. Defaults to the current time if not provided.  | [optional]  |
| **campaignId** | **string** | The ID of the campaign to filter metrics by. If not provided, metrics for all campaigns will be returned.  | [optional]  |
| **interval** | **string** | The interval for aggregating metrics data. Allowed values:   - hour: Hourly breakdown   - day: Daily breakdown (default)   - week: Weekly breakdown   - month: Monthly breakdown  | [optional] [default to day] |

### Return type

[**MetricsRecipientBehaviour**](MetricsRecipientBehaviour.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successfully retrieved recipient behaviour metrics  |  -  |
| **400** | Bad Request |  -  |
| **500** | Internal Server Error |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="getsendermetrics"></a>
# **GetSenderMetrics**
> MetricsSenderResponse GetSenderMetrics (string senderType, string xApiKey, string startTime = null, string endTime = null, int limit = null, int offset = null, string sortOrder = null)

Retrieve Sender Metrics

Retrieves a list of senders, either sub-accounts or campaigns, with their associated message metrics. Sorted by total # of sent messages (processed + dropped) Supports optional filter for time range, and optional settings for limit, offset, and sort order. Note: senders without any messages in the given time range will not be included in the results. The default time range is from one month ago to now, and the default sort order is descending. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **senderType** | **string** |  |  |
| **xApiKey** | **string** |  |  |
| **startTime** | **string** | The beginning of the time range for retrieving top senders metrics (inclusive). Formats: YYYY-MM-DD or YYYY-MM-DDTHH:MM:SSZ Defaults to one month ago if not provided.  | [optional]  |
| **endTime** | **string** | The end of the time range for retrieving top senders metrics (exclusive). Formats: YYYY-MM-DD or YYYY-MM-DDTHH:MM:SSZ Defaults to the current time if not provided.  | [optional]  |
| **limit** | **int** | The maximum number of senders to return The default is 10.  | [optional] [default to 10] |
| **offset** | **int** | The number of senders to skip before returning results.  | [optional] [default to 0] |
| **sortOrder** | **string** | The order in which to sort the results, based on total messages (processed + dropped).  | [optional] [default to desc] |

### Return type

[**MetricsSenderResponse**](MetricsSenderResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successfully retrieved top senders metrics  |  -  |
| **400** | Invalid request |  -  |
| **500** | Internal server error |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="getvolumemetrics"></a>
# **GetVolumeMetrics**
> MetricsVolume GetVolumeMetrics (string xApiKey, string startTime = null, string endTime = null, string campaignId = null, string interval = null)

Retrieve Volume Metrics

Retrieve volume metrics for messages sent from your account, including counts of processed, delivered and dropped events. Supports optional filters for time range and campaign ID. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **xApiKey** | **string** |  |  |
| **startTime** | **string** | The beginning of the time range for retrieving message volume metrics (inclusive). Formats: YYYY-MM-DD or YYYY-MM-DDTHH:MM:SSZ. Defaults to one month ago if not provided.  | [optional]  |
| **endTime** | **string** | The end of the time range for retrieving message volume metrics (exclusive). Formats: YYYY-MM-DD or YYYY-MM-DDTHH:MM:SSZ. Defaults to the current time if not provided.  | [optional]  |
| **campaignId** | **string** | The ID of the campaign to filter metrics by. If not provided, metrics for all campaigns will be returned.  | [optional]  |
| **interval** | **string** | The interval for aggregating metrics data. Allowed values:   - hour: Hourly breakdown   - day: Daily breakdown (default)   - week: Weekly breakdown   - month: Monthly breakdown  | [optional] [default to day] |

### Return type

[**MetricsVolume**](MetricsVolume.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successfully retrieved volume metrics  |  -  |
| **400** | Bad Request |  -  |
| **500** | Internal Server Error |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

