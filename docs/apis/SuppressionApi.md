# MailChannels.EmailApi.Api.SuppressionApi

All URIs are relative to *https://api.mailchannels.net/tx/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateSuppressions**](SuppressionApi.md#createsuppressions) | **POST** /suppression-list | Create Suppression Entries |
| [**DeleteSuppression**](SuppressionApi.md#deletesuppression) | **DELETE** /suppression-list/recipients/{recipient} | Delete Suppression Entry |
| [**ListSuppressions**](SuppressionApi.md#listsuppressions) | **GET** /suppression-list | Retrieve Suppression List |

<a id="createsuppressions"></a>
# **CreateSuppressions**
> void CreateSuppressions (string xApiKey, SuppressionListInput suppressionListInput)

Create Suppression Entries

Creates suppression entries for the specified account. Parent accounts can create suppression entries for all associated sub-accounts. If suppression_type is not provided, it defaults to 'non-transactional'. The operation is atomic, meaning all entries are successfully added or none are added if an error occurs. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **xApiKey** | **string** |  |  |
| **suppressionListInput** | [**SuppressionListInput**](SuppressionListInput.md) | The details of the suppression entries to create. |  |

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | All suppression entries were successfully created. |  -  |
| **400** | Bad request. The request body is invalid. |  -  |
| **409** | Conflicts. One or more suppression entries in the request already exist and cannot be created again.  |  -  |
| **413** | Payload too large. The request exceeds the maximum allowed total of 1000 suppression entries for the parent account and/or its sub-accounts.  |  -  |
| **500** | An unexpected internal error occurred. |  -  |
| **503** | Temporarily unavailable for maintenance. |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="deletesuppression"></a>
# **DeleteSuppression**
> void DeleteSuppression (string recipient, string xApiKey, string source = null)

Delete Suppression Entry

Deletes suppression entry associated with the account based on the specified recipient and source. If source is not provided, it defaults to 'api'. If source is set to 'all', all suppression entries related to the specified recipient will be deleted. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **recipient** | **string** |  |  |
| **xApiKey** | **string** |  |  |
| **source** | **string** |  | [optional] [default to api] |

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** | The specified suppression entry was successfully deleted. |  -  |
| **400** | Bad request. The request is invalid. |  -  |
| **500** | An unexpected internal error occurred. |  -  |
| **503** | Temporarily unavailable for maintenance. |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="listsuppressions"></a>
# **ListSuppressions**
> SuppressionListResponse ListSuppressions (string xApiKey, string recipient = null, string source = null, string createdBefore = null, string createdAfter = null, int limit = null, int offset = null)

Retrieve Suppression List

Retrieve suppression entries associated with the specified account. Supports filtering by recipient, source and creation date range. The response is paginated, with a default limit of 1000 entries per page and an offset of 0. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **xApiKey** | **string** |  |  |
| **recipient** | **string** |  | [optional]  |
| **source** | **string** |  | [optional]  |
| **createdBefore** | **string** | The date and/or time before which the suppression entries were created. Format: YYYY-MM-DD or YYYY-MM-DDTHH:MM:SSZ  | [optional]  |
| **createdAfter** | **string** | The date and/or time after which the suppression entries were created. Format: YYYY-MM-DD or YYYY-MM-DDTHH:MM:SSZ  | [optional]  |
| **limit** | **int** | The maximum number of suppression entries to return. The default is 1000.  | [optional] [default to 1000] |
| **offset** | **int** | The number of suppression entries to skip before returning results. The default is 0.  | [optional] [default to 0] |

### Return type

[**SuppressionListResponse**](SuppressionListResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successfully retrieved all suppression entries associated with the account. |  -  |
| **400** | Bad request. The request is invalid. |  -  |
| **500** | An unexpected internal error occurred. |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

