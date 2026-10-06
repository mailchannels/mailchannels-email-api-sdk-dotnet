# MailChannels.EmailApi.Model.UsageStats

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**MonthlyLimit** | **int** | The effective monthly limit for the current billing period. A limit of zero means the account cannot send any messages. For sub-accounts with no explicit limit set (i.e., -1), the monthly limit for the parent account is returned.  | 
**TotalUsage** | **long** | The total usage for the current billing period. | 
**PeriodEndDate** | **DateOnly** | The end date of the current billing period (ISO 8601 format). | [optional] 
**PeriodStartDate** | **DateOnly** | The start date of the current billing period (ISO 8601 format). | [optional] 

[[Back to Model list]](../../README.md#documentation-for-models) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to README]](../../README.md)

