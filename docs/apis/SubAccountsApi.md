# MailChannels.EmailApi.Api.SubAccountsApi

All URIs are relative to *https://api.mailchannels.net/tx/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ActivateSubaccount**](SubAccountsApi.md#activatesubaccount) | **POST** /sub-account/{handle}/activate | Activate Sub-account |
| [**CreateSubaccount**](SubAccountsApi.md#createsubaccount) | **POST** /sub-account | Create Sub-account |
| [**CreateSubaccountApiKey**](SubAccountsApi.md#createsubaccountapikey) | **POST** /sub-account/{handle}/api-key | Create Sub-account API Key |
| [**CreateSubaccountSmtpPassword**](SubAccountsApi.md#createsubaccountsmtppassword) | **POST** /sub-account/{handle}/smtp-password | Create Sub-account SMTP Password |
| [**DeleteSubaccount**](SubAccountsApi.md#deletesubaccount) | **DELETE** /sub-account/{handle} | Delete Sub-account |
| [**DeleteSubaccountApiKey**](SubAccountsApi.md#deletesubaccountapikey) | **DELETE** /sub-account/{handle}/api-key/{id} | Delete Sub-account API Key |
| [**DeleteSubaccountLimit**](SubAccountsApi.md#deletesubaccountlimit) | **DELETE** /sub-account/{handle}/limit | Delete Sub-account Limit |
| [**DeleteSubaccountSmtpPassword**](SubAccountsApi.md#deletesubaccountsmtppassword) | **DELETE** /sub-account/{handle}/smtp-password/{id} | Delete Sub-account SMTP Password |
| [**GetSubaccountLimit**](SubAccountsApi.md#getsubaccountlimit) | **GET** /sub-account/{handle}/limit | Retrieve Sub-account Limit |
| [**GetSubaccountUsage**](SubAccountsApi.md#getsubaccountusage) | **GET** /sub-account/{handle}/usage | Retrieve Sub-account Usage Stats |
| [**ListSubaccountApiKeys**](SubAccountsApi.md#listsubaccountapikeys) | **GET** /sub-account/{handle}/api-key | Retrieve Sub-account API Keys |
| [**ListSubaccountSmtpPasswords**](SubAccountsApi.md#listsubaccountsmtppasswords) | **GET** /sub-account/{handle}/smtp-password | Retrieve Sub-account SMTP Passwords |
| [**ListSubaccounts**](SubAccountsApi.md#listsubaccounts) | **GET** /sub-account | Retrieve Sub-accounts |
| [**SetSubaccountLimit**](SubAccountsApi.md#setsubaccountlimit) | **PUT** /sub-account/{handle}/limit | Set Sub-account Limit |
| [**SuspendSubaccount**](SubAccountsApi.md#suspendsubaccount) | **POST** /sub-account/{handle}/suspend | Suspend Sub-account |

<a id="activatesubaccount"></a>
# **ActivateSubaccount**
> void ActivateSubaccount (string handle, string xApiKey)

Activate Sub-account

Activates a suspended sub-account identified by its handle, restoring its ability to send emails. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **handle** | **string** | Handle of sub-account to be activated. |  |
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
| **204** | The specified sub-account is successfully activated. |  -  |
| **403** | The operation is forbidden. The parent account does not have permission to activate the sub-account. |  -  |
| **404** | The specified sub-account does not exist. |  -  |
| **500** | An unexpected internal error occurred. |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="createsubaccount"></a>
# **CreateSubaccount**
> SubAccountDetails CreateSubaccount (string xApiKey, SubAccountData subAccountData = null)

Create Sub-account

Creates a new sub-account under the parent account. Each sub-account must have a unique handle composed solely of lowercase alphanumeric characters. If no handle is provided, a random handle will be generated. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **xApiKey** | **string** |  |  |
| **subAccountData** | [**SubAccountData**](SubAccountData.md) | The details of the sub-account to create. | [optional]  |

### Return type

[**SubAccountDetails**](SubAccountDetails.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | The sub-account was successfully created. |  -  |
| **400** | Malformed request. The request body is invalid. |  -  |
| **403** | The operation is forbidden. The parent account does not have permission to create sub-accounts. |  -  |
| **409** | A sub-account with the specified name already exists. |  -  |
| **500** | An unexpected internal error occurred. |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="createsubaccountapikey"></a>
# **CreateSubaccountApiKey**
> APIKey CreateSubaccountApiKey (string handle, string xApiKey)

Create Sub-account API Key

Creates a new API key for the specified sub-account. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **handle** | **string** | Handle of the sub-account to create API key for. |  |
| **xApiKey** | **string** |  |  |

### Return type

[**APIKey**](APIKey.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | A new API key was successfully created for the specified sub-account.  |  -  |
| **403** | The operation is forbidden. You can&#39;t create API keys for this sub-account.  |  -  |
| **404** | The specified sub-account does not exist.  |  -  |
| **422** | You have reached the limit of API keys you can create for this sub-account.  |  -  |
| **500** | An unexpected internal error occurred.  |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="createsubaccountsmtppassword"></a>
# **CreateSubaccountSmtpPassword**
> SMTPPassword CreateSubaccountSmtpPassword (string handle, string xApiKey)

Create Sub-account SMTP Password

Creates a new SMTP password for the specified sub-account. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **handle** | **string** | Handle of the sub-account to create SMTP password for. |  |
| **xApiKey** | **string** |  |  |

### Return type

[**SMTPPassword**](SMTPPassword.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | A new SMTP password was successfully created for the specified sub-account. |  -  |
| **403** | The operation is forbidden. You can&#39;t create SMTP passwords for this sub-account.  |  -  |
| **404** | The specified sub-account does not exist. |  -  |
| **422** | You have reached the limit of SMTP passwords you can create for this sub-account.  |  -  |
| **500** | An unexpected internal error occurred. |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="deletesubaccount"></a>
# **DeleteSubaccount**
> void DeleteSubaccount (string handle, string xApiKey)

Delete Sub-account

Deletes the sub-account identified by its handle.


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **handle** | **string** | Handle of sub-account to be deleted. |  |
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
| **204** | The specified sub-account(s) were successfully deleted. |  -  |
| **500** | An unexpected internal error occurred. |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="deletesubaccountapikey"></a>
# **DeleteSubaccountApiKey**
> void DeleteSubaccountApiKey (string handle, int id, string xApiKey)

Delete Sub-account API Key

Deletes the API key identified by its ID for the specified sub-account. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **handle** | **string** | Handle of the sub-account for which the API key should be deleted.  |  |
| **id** | **int** | The ID of the API key to delete. |  |
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
| **204** | The API key was successfully deleted for the sub-account.  |  -  |
| **400** | Missing or invalid API key ID.  |  -  |
| **500** | An unexpected internal error occurred.  |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="deletesubaccountlimit"></a>
# **DeleteSubaccountLimit**
> void DeleteSubaccountLimit (string handle, string xApiKey)

Delete Sub-account Limit

Deletes the limit for the specified sub-account. After a successful deletion, the specified sub-account will be limited to the parent account's limit. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **handle** | **string** | Handle of the sub-account to delete limit for. |  |
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
| **204** | The limit was successfully deleted for the sub-account. |  -  |
| **500** | An unexpected internal error occurred. |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="deletesubaccountsmtppassword"></a>
# **DeleteSubaccountSmtpPassword**
> void DeleteSubaccountSmtpPassword (string handle, int id, string xApiKey)

Delete Sub-account SMTP Password

Deletes the SMTP password identified by its ID for the specified sub-account. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **handle** | **string** | Handle of the sub-account for which the SMTP password should be deleted. |  |
| **id** | **int** | The ID of the SMTP password to delete. |  |
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
| **204** | The specified SMTP password was successfully deleted for the sub-account. |  -  |
| **400** | Missing or invalid SMTP password ID. |  -  |
| **500** | An unexpected internal error occurred. |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="getsubaccountlimit"></a>
# **GetSubaccountLimit**
> Limit GetSubaccountLimit (string handle, string xApiKey)

Retrieve Sub-account Limit

Retrieves the limit of a specified sub-account. A value of -1 indicates that the sub-account inherits the parent account's limit, allowing the sub-account to utilize any remaining capacity within the parent account's allocation. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **handle** | **string** | Handle of the sub-account to retrieve the limit for. |  |
| **xApiKey** | **string** |  |  |

### Return type

[**Limit**](Limit.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successfully retrieved the limit for the specified sub-account. |  -  |
| **404** | The specified sub-account does not exist. |  -  |
| **500** | An unexpected internal error occurred. |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="getsubaccountusage"></a>
# **GetSubaccountUsage**
> UsageStats GetSubaccountUsage (string handle, string xApiKey)

Retrieve Sub-account Usage Stats

Retrieves usage statistics for the specified sub-account during the current billing period.


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **handle** | **string** | Handle of the sub-account to query usage stats for. |  |
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
| **404** | Sub-account not found |  -  |
| **500** | Internal Server Error |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="listsubaccountapikeys"></a>
# **ListSubaccountApiKeys**
> List&lt;APIKey&gt; ListSubaccountApiKeys (string handle, string xApiKey, int limit = null, int offset = null)

Retrieve Sub-account API Keys

Retrieves details of all API keys associated with the specified sub-account. For security reasons, the full API key is **not** returned; only the key ID and a partially redacted version are provided. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **handle** | **string** | Handle of the sub-account to retrieve the API key for. |  |
| **xApiKey** | **string** |  |  |
| **limit** | **int** |  | [optional] [default to 100] |
| **offset** | **int** |  | [optional] [default to 0] |

### Return type

[**List&lt;APIKey&gt;**](APIKey.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successfully retrieved the API key for the specified sub-account. |  -  |
| **404** | The specified sub-account does not exist. |  -  |
| **500** | An unexpected internal error occurred. |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="listsubaccountsmtppasswords"></a>
# **ListSubaccountSmtpPasswords**
> List&lt;SMTPPassword&gt; ListSubaccountSmtpPasswords (string handle, string xApiKey)

Retrieve Sub-account SMTP Passwords

Retrieves details of all SMTP passwords associated with the specified sub-account. For security, the full SMTP password is **not** returned; only the password ID and a partially redacted version are provided. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **handle** | **string** | Handle of the sub-account to retrieve the SMTP password for. |  |
| **xApiKey** | **string** |  |  |

### Return type

[**List&lt;SMTPPassword&gt;**](SMTPPassword.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successfully retrieved the SMTP password for the specified sub-account. |  -  |
| **404** | The specified sub-account does not exist. |  -  |
| **500** | An unexpected internal error occurred. |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="listsubaccounts"></a>
# **ListSubaccounts**
> List&lt;SubAccountDetails&gt; ListSubaccounts (string xApiKey, int limit = null, int offset = null)

Retrieve Sub-accounts

Retrieves all sub-accounts associated with the parent account. The response is paginated with a default limit of 1000 sub-accounts per page and an offset of 0. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **xApiKey** | **string** |  |  |
| **limit** | **int** |  | [optional] [default to 1000] |
| **offset** | **int** |  | [optional] [default to 0] |

### Return type

[**List&lt;SubAccountDetails&gt;**](SubAccountDetails.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successfully retrieved all sub-accounts associated with the parent account. |  -  |
| **400** | Bad Request. The limit and/or offset query parameter are invalid. |  -  |
| **500** | An unexpected internal error occurred. |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="setsubaccountlimit"></a>
# **SetSubaccountLimit**
> LimitUpdateResult SetSubaccountLimit (string handle, string xApiKey, LimitInput limitInput)

Set Sub-account Limit

Sets the limit for the specified sub-account. The minimum allowed sends is 0. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **handle** | **string** | Handle of the sub-account to set limit for. |  |
| **xApiKey** | **string** |  |  |
| **limitInput** | [**LimitInput**](LimitInput.md) | The value the sub-account limit to set. |  |

### Return type

[**LimitUpdateResult**](LimitUpdateResult.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | The limit was successfully updated for the specified sub-account. |  -  |
| **400** | Missing or invalid limit value.  |  -  |
| **404** | The specified sub-account does not exist. |  -  |
| **500** | An unexpected internal error occurred. |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="suspendsubaccount"></a>
# **SuspendSubaccount**
> void SuspendSubaccount (string handle, string xApiKey)

Suspend Sub-account

Suspends the sub-account identified by its handle. This action disables the account, preventing it from sending any emails until it is reactivated. 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **handle** | **string** | Handle of sub-account to be suspended. |  |
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
| **204** | The specified sub-account is successfully suspended. |  -  |
| **404** | The specified sub-account does not exist. |  -  |
| **500** | An unexpected internal error occurred. |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

