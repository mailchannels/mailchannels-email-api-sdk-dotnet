# MailChannels.EmailApi.Api.CustomTrackingApi

All URIs are relative to *https://api.mailchannels.net/tx/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateCustomTrackingDomain**](CustomTrackingApi.md#createcustomtrackingdomain) | **POST** /custom-tracking-domains | Register Custom Tracking Domain |
| [**DeleteCustomTrackingDomain**](CustomTrackingApi.md#deletecustomtrackingdomain) | **DELETE** /custom-tracking-domains/{hostname}/{scope} | Delete Custom Tracking Domain |
| [**ListCustomTrackingDomains**](CustomTrackingApi.md#listcustomtrackingdomains) | **GET** /custom-tracking-domains | Retrieve Custom Tracking Domains |
| [**UpdateCustomTrackingDomain**](CustomTrackingApi.md#updatecustomtrackingdomain) | **PATCH** /custom-tracking-domains/{hostname}/{scope} | Update Custom Tracking Domain |

<a id="createcustomtrackingdomain"></a>
# **CreateCustomTrackingDomain**
> CustomTrackingDomain CreateCustomTrackingDomain (string xApiKey, PostCustomTrackingDomainRequest postCustomTrackingDomainRequest)

Register Custom Tracking Domain

Register a custom branded domain for click tracking, open tracking, or unsubscribe handling. By default, MailChannels uses shared domains for these links. Using a custom domain improves brand consistency by replacing shared domains with your own (e.g., click.example.com). Once registered, select the domain at send time using its `name`.  Before registration completes, two DNS records must be in place: 1. A TXT record at `_mailchannels-verify.<hostname>` containing the verification token    (returned in the 202 response). 2. A CNAME record at `<hostname>` pointing to `links.mailchannels.net`. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **xApiKey** | **string** |  |  |
| **postCustomTrackingDomainRequest** | [**PostCustomTrackingDomainRequest**](PostCustomTrackingDomainRequest.md) |  |  |

### Return type

[**CustomTrackingDomain**](CustomTrackingDomain.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | Domain verified and registered |  -  |
| **202** | Verification required — add the DNS TXT record and CNAME record described in the response body, then retry |  -  |
| **400** | Invalid request body |  -  |
| **403** | No permission to register this domain |  -  |
| **409** | A domain with the same name already exists, or the hostname and scope combination is already registered |  -  |
| **422** | DNS verification incomplete. Either the TXT ownership record has not propagated yet or the hostname CNAME does not point to the required target. Check the &#x60;instructions&#x60; field and retry once both records are in place.  |  -  |
| **500** | Internal Server Error |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="deletecustomtrackingdomain"></a>
# **DeleteCustomTrackingDomain**
> void DeleteCustomTrackingDomain (string hostname, string scope, string xApiKey)

Delete Custom Tracking Domain

Permanently delete an existing custom tracking domain for the given hostname and scope. The domain can be re-registered if needed. WARNING: Any tracking links or unsubscribe URLs in previously sent emails using this domain will stop working immediately. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **hostname** | **string** |  |  |
| **scope** | **string** |  |  |
| **xApiKey** | **string** |  |  |

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
| **204** | Custom tracking domain successfully deleted |  -  |
| **400** | Invalid hostname or scope value |  -  |
| **500** | Internal server error |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="listcustomtrackingdomains"></a>
# **ListCustomTrackingDomains**
> CustomTrackingDomainListResponse ListCustomTrackingDomains (string xApiKey, string name = null, string status = null, string scope = null, int limit = null, int offset = null)

Retrieve Custom Tracking Domains

Retrieve all custom tracking domains registered under your account. Optional filters include domain name, status, scope, limit and offset. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **xApiKey** | **string** |  |  |
| **name** | **string** | Filter by custom tracking domain label | [optional]  |
| **status** | **string** | Filter by status | [optional]  |
| **scope** | **string** | Filter by scope | [optional]  |
| **limit** | **int** | The maximum number of domains to return. The default is 100. | [optional] [default to 100] |
| **offset** | **int** | The number of domains to skip before returning results. The default is 0. | [optional] [default to 0] |

### Return type

[**CustomTrackingDomainListResponse**](CustomTrackingDomainListResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Response with the list of custom tracking domains |  -  |
| **400** | Invalid query parameter value |  -  |
| **500** | Internal server error |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="updatecustomtrackingdomain"></a>
# **UpdateCustomTrackingDomain**
> CustomTrackingDomain UpdateCustomTrackingDomain (string hostname, string scope, string xApiKey, PatchCustomTrackingDomainRequest patchCustomTrackingDomainRequest)

Update Custom Tracking Domain

Update an existing custom tracking domain by its hostname and scope. Supports updating the custom tracking domain's name or toggling its active status. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **hostname** | **string** |  |  |
| **scope** | **string** |  |  |
| **xApiKey** | **string** |  |  |
| **patchCustomTrackingDomainRequest** | [**PatchCustomTrackingDomainRequest**](PatchCustomTrackingDomainRequest.md) |  |  |

### Return type

[**CustomTrackingDomain**](CustomTrackingDomain.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Custom tracking domain updated |  -  |
| **202** | Re-activation requires DNS verification — add the TXT record and CNAME record described in the response body, then retry |  -  |
| **400** | Invalid request body |  -  |
| **403** | No permission to activate this domain |  -  |
| **404** | Domain not found |  -  |
| **409** | Name already used by another domain |  -  |
| **422** | DNS verification incomplete. Either the TXT ownership record has not propagated yet or the hostname CNAME does not point to the required target. Check the &#x60;instructions&#x60; field and retry once both records are in place.  |  -  |
| **500** | Internal server error |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

