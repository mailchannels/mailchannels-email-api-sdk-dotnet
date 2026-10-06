# MailChannels.EmailApi.Model.Attachment

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Content** | **string** | the attachment data, encoded in base64 | 
**Filename** | **string** | the name of the attachment file | 
**ContentId** | **string** | A unique identifier for this attachment. When set, the attachment is embedded inline in the message body (Content-Disposition: inline) instead of offered as a downloadable attachment, and can be referenced from HTML content via a &#x60;cid:&#x60; URI, e.g. &#x60;&lt;img src&#x3D;\&quot;cid:logo123\&quot;&gt;&#x60; refers to an attachment with &#x60;content_id: logo123&#x60; (RFC 2392). Must be unique across all attachments in the request.  | [optional] 
**Type** | **string** | the MIME type of the attachment | [optional] 

[[Back to Model list]](../../README.md#documentation-for-models) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to README]](../../README.md)

