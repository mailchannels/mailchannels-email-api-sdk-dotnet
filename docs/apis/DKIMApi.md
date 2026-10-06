# MailChannels.EmailApi.Api.DKIMApi

All URIs are relative to *https://api.mailchannels.net/tx/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CheckDomain**](DKIMApi.md#checkdomain) | **POST** /check-domain | DKIM, SPF &amp; Domain Lockdown Check |
| [**CreateDkimKey**](DKIMApi.md#createdkimkey) | **POST** /domains/{domain}/dkim-keys | Create DKIM Key Pair |
| [**ListDkimKeys**](DKIMApi.md#listdkimkeys) | **GET** /domains/{domain}/dkim-keys | Retrieve DKIM Keys |
| [**RotateDkimKey**](DKIMApi.md#rotatedkimkey) | **POST** /domains/{domain}/dkim-keys/{selector}/rotate | Rotate DKIM Key Pair |
| [**UpdateDkimKey**](DKIMApi.md#updatedkimkey) | **PATCH** /domains/{domain}/dkim-keys/{selector} | Update DKIM Key Status |

<a id="checkdomain"></a>
# **CheckDomain**
> CheckDomainResult CheckDomain (string xApiKey, CheckDomainBody checkDomainBody)

DKIM, SPF & Domain Lockdown Check

Validates a domain's email authentication setup by retrieving its DKIM, SPF, and Domain Lockdown status. This endpoint checks whether the domain is properly configured for secure email delivery. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **xApiKey** | **string** |  |  |
| **checkDomainBody** | [**CheckDomainBody**](CheckDomainBody.md) |  |  |

### Return type

[**CheckDomainResult**](CheckDomainResult.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Success. |  -  |
| **400** | Bad Request |  -  |
| **403** | User does not have access to this feature |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="createdkimkey"></a>
# **CreateDkimKey**
> DKIMKeyInfo CreateDkimKey (string domain, string xApiKey, DKIMKeyPairCreateRequest dKIMKeyPairCreateRequest)

Create DKIM Key Pair

Create a DKIM key pair for a specified domain and selector using the specified algorithm and key length, for the current customer. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **domain** | **string** |  |  |
| **xApiKey** | **string** |  |  |
| **dKIMKeyPairCreateRequest** | [**DKIMKeyPairCreateRequest**](DKIMKeyPairCreateRequest.md) |  |  |

### Return type

[**DKIMKeyInfo**](DKIMKeyInfo.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | Key pair created successfully |  -  |
| **400** | Bad Request |  -  |
| **409** | Key pair already created for domain, and selector  |  -  |
| **500** | Internal Server Error |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="listdkimkeys"></a>
# **ListDkimKeys**
> DKIMKeyList ListDkimKeys (string domain, string xApiKey, string selector = null, string status = null, int offset = null, int limit = null, bool includeDnsRecord = null)

Retrieve DKIM Keys

Search for DKIM keys by domain, with optional filters. If selector is provided, at most one key will be returned. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **domain** | **string** |  |  |
| **xApiKey** | **string** |  |  |
| **selector** | **string** |  | [optional]  |
| **status** | **string** |  | [optional]  |
| **offset** | **int** | Number of keys to skip before returning results. The default is 0.  | [optional] [default to 0] |
| **limit** | **int** | Maximum number of keys to return. The default is 10.  | [optional] [default to 10] |
| **includeDnsRecord** | **bool** | If true, includes the suggested DKIM DNS record for each returned key. Defaults to false.  | [optional] [default to false] |

### Return type

[**DKIMKeyList**](DKIMKeyList.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successfully retrieved DKIM keys |  -  |
| **400** | Bad Request |  -  |
| **500** | Internal Server Error |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="rotatedkimkey"></a>
# **RotateDkimKey**
> DKIMKeyRotateResponse RotateDkimKey (string domain, string selector, string xApiKey, DKIMKeyRotateRequest dKIMKeyRotateRequest)

Rotate DKIM Key Pair

Rotate an active DKIM key pair. Mark the original key as 'rotated', and create a new key pair with the required new key selector, reusing the same algorithm and key length. The rotated key remains valid for signing for a 3-day grace period, and is automatically changed to 'retired' 2 weeks after rotation. Publish the new key to its DNS TXT record before rotated key expires for signing as emails sent with an unpublished key will fail DKIM validation by receiving providers. After the grace period, only the new key is valid for signing if published.


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **domain** | **string** |  |  |
| **selector** | **string** |  |  |
| **xApiKey** | **string** |  |  |
| **dKIMKeyRotateRequest** | [**DKIMKeyRotateRequest**](DKIMKeyRotateRequest.md) |  |  |

### Return type

[**DKIMKeyRotateResponse**](DKIMKeyRotateResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | Key pair status updated and new key pair created successfully |  -  |
| **400** | Bad Request |  -  |
| **404** | Specified key pair not found |  -  |
| **409** | Key pair already created for domain, and provided new key selector.  |  -  |
| **500** | Internal Server Error |  -  |
| **503** | Temporarily unavailable for maintenance |  * Retry-After - Suggested wait time in seconds before retrying.  <br>  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="updatedkimkey"></a>
# **UpdateDkimKey**
> void UpdateDkimKey (string domain, string selector, string xApiKey, DKIMKeyPairUpdateRequest dKIMKeyPairUpdateRequest)

Update DKIM Key Status

Update fields of an existing DKIM key pair for the specified domain and selector, for the current customer. Currently, only the status field can be updated. revoked: Indicates that the key is compromised and should not be used. retired: Indicates that the key has been rotated and is no longer in use. rotated: Indicates that the key is going through the rotation process. Only active key pairs can be updated to this status, and no new key pair is created. The rotated key can be used to sign emails for 3 days after the status update, and will automatically change to 'retired' 2 weeks after update. For a smooth key transition, it is recommended to create and publish a new key pair before signing is disabled for the rotated key. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **domain** | **string** |  |  |
| **selector** | **string** |  |  |
| **xApiKey** | **string** |  |  |
| **dKIMKeyPairUpdateRequest** | [**DKIMKeyPairUpdateRequest**](DKIMKeyPairUpdateRequest.md) |  |  |

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
| **204** | Key pair status updated successfully |  -  |
| **400** | Status not supported |  -  |
| **404** | Specified key pair not found, or no active key for rotation This may also occur if the DKIM domain or selector path parameter is missing.  |  -  |
| **500** | Internal Server Error |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

