# MailChannels.EmailApi.Model.DKIMKeyPairCreateRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Selector** | **string** | Selector for the new key pair  | 
**Algorithm** | **string** | Algorithm used for the new key pair Currently, only RSA is supported.  | [optional] [default to AlgorithmEnum.Rsa]
**KeyLength** | **int** | Key length in bits. For RSA, must be a multiple of 1024. Common values: 1024 or 2048. Defaults to 2048 bits.  | [optional] [default to 2048]

[[Back to Model list]](../../README.md#documentation-for-models) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to README]](../../README.md)

