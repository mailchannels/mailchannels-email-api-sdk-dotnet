# MailChannels Email API for .NET

Unreleased candidate. This package has not been published to NuGet.org.
Support: dev@mailchannels.com. License: MIT.

A server-side client covering all 42 operations in Email API 1.7.1: sending,
subaccounts, webhooks, suppressions, metrics, domain/DKIM and tracking management.
Never place API keys in browser or mobile applications.

The library targets net8.0 and has 65 passing native fixture checks on Linux
with .NET 8, 9 and 10 consumers. Prefer .NET 10 for new applications: Microsoft's
support for .NET 8/9 ends November 10, 2026. Windows/macOS remain unvalidated.
Fixtures are baseline contract checks, not exhaustive live-provider conformance.

## Use

Register clients through `services.AddApi()` from `MailChannels.EmailApi.Extensions`
and resolve `ISendApi` from dependency injection. `SendEmailAsync` calls `/send`;
`QueueEmailAsync` calls `/send-async`. Supply the API key per call from secure
application configuration. Pass `dryRun: true` to request provider dry-run
validation; this still makes a real API request and requires an authorized key.

Inspect `StatusCode` and `IsSuccessStatusCode`. For a send response, `Ok()` returns
the HTTP 200 dry-run model and `Accepted()` returns HTTP 202 per-personalization
results. Inspect each result: HTTP success does not establish that every message
succeeded. Wrong-status accessors return null. Malformed JSON throws from direct
accessors; `Try...` accessors return false. RawContent is explicit unredacted data.

## Transport and diagnostics

Registered clients require HTTPS, disable redirects and cookies, and default to
10-second connection and 30-second total request timeouts. Certificate checks
remain enabled. No retry policy is installed by default. An interrupted send can
have an unknown delivery outcome; do not blindly resend. Trusted application
configuration can replace handlers or add retry policies. Constructing API
classes manually bypasses these registered defaults.

Model ToString output is redacted and default SDK error logs omit raw exception
objects. Explicit fields, serialization, raw responses, thrown exceptions and
event callbacks retain data. Avoid logging those values or credentials.

## Release status

NuGet ownership, release review and registry-install verification remain pending.
Do not infer registry availability from the local evaluation package.
