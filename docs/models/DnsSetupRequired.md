# MailChannels.EmailApi.Model.DnsSetupRequired

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Instructions** | **string** | Human-readable guidance for the DNS records that must be in place before retrying. | [optional] 
**Token** | **string** | UUID v4 nonce; also the TXT record value to set. Present only when TXT ownership verification is pending. | [optional] 
**TxtRecordName** | **string** | Fully-qualified DNS TXT record name to add. Present only when TXT ownership verification is pending. | [optional] 
**TxtRecordValue** | **string** | Value for the DNS TXT record (same as token). Present only when TXT ownership verification is pending. | [optional] 

[[Back to Model list]](../../README.md#documentation-for-models) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to README]](../../README.md)

