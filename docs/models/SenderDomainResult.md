# MailChannels.EmailApi.Model.SenderDomainResult
These results are here to help avoid SDNF (Sender Domain Not Found) blocks. For messages not to get blocked by [SDNF](https://support.mailchannels.com/hc/en-us/articles/203155500-550-5-2-1-SDNF-Sender-Domain-Not-Found), we require either an MX or A record to exist for the sender domain. 

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**A** | [**SenderDomainResultA**](SenderDomainResultA.md) |  | [optional] 
**Mx** | [**SenderDomainResultMx**](SenderDomainResultMx.md) |  | [optional] 
**Verdict** | **string** | Overall verdict. Passed if either A or MX record check passed. | [optional] 

[[Back to Model list]](../../README.md#documentation-for-models) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to README]](../../README.md)

