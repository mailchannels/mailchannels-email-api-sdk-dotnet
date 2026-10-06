# MailChannels.EmailApi.Model.DkimResult

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**DkimDomain** | **string** |  | [optional] 
**DkimKeyStatus** | **string** | The human readable status of the DKIM key used for verification. This field is only present if the DKIM check was performed using a DKIM key managed by MailChannels. If a DKIM key is present in the request, this field will not be included.  | [optional] 
**DkimSelector** | **string** |  | [optional] 
**Reason** | **string** | A human-readable explanation of DKIM check. | [optional] 
**Verdict** | **string** |  | [optional] 

[[Back to Model list]](../../README.md#documentation-for-models) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to README]](../../README.md)

