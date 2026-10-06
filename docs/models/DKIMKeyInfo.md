# MailChannels.EmailApi.Model.DKIMKeyInfo

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Algorithm** | **string** | Algorithm used for the key pair  | 
**Domain** | **string** | Domain associated with the key pair  | 
**PublicKey** | **string** |  | 
**Selector** | **string** | Selector assigned to the key pair  | 
**Status** | **string** |  | 
**CreatedAt** | **DateTime** | Timestamp when the key pair was created  | [optional] 
**DkimDnsRecords** | [**List&lt;DKIMDnsRecord&gt;**](DKIMDnsRecord.md) | Suggested DNS records for the DKIM key  | [optional] 
**GracePeriodExpiresAt** | **DateTime** | UTC timestamp after which you can no longer use the rotated key for signing  | [optional] 
**KeyLength** | **int** | Key length in bits  | [optional] 
**RetiresAt** | **DateTime** | UTC timestamp when a rotated key pair is retired  | [optional] 
**StatusModifiedAt** | **DateTime** | Timestamp when the key was last modified  | [optional] 

[[Back to Model list]](../../README.md#documentation-for-models) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to README]](../../README.md)

