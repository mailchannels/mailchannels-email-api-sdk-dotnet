C# generator correction

JsonConverter.mustache is derived from OpenAPI Generator v7.26.0:
modules/openapi-generator/src/main/resources/csharp/libraries/generichost/JsonConverter.mustache
License: Apache-2.0; see LICENSE.openapitools.

The upstream nonnumeric inner-enum writer dereferences an unset nullable
Option.Value, so ordinary MailSendBody serialization crashes when ContentItem's
optional template_type is omitted. The customized writer guards optional values
with IsSet, retains explicit-null rejection for nonnullable properties, and
writes null for nullable properties without dereferencing a missing enum.
Required fields retain unconditional serialization.

The handler-fixture executable tests ordinary email serialization, explicit
Mustache template type and rejected explicit null. These are runtime client
checks with a fake HTTP handler; they do not validate sockets/TLS/redirects.

Diagnostic corrections: modelGeneric.mustache uses a type-name/redaction marker
for ToString instead of enumerating field values. OnErrorDefaultImplementation
and OnDeserializationError retain event IDs and generic messages but omit raw
exceptions from default SDK log calls. All three templates derive from the same
upstream v7.26.0 generichost directory under Apache-2.0. Explicit properties and
JSON serialization retain payloads; thrown exceptions and event callbacks retain
raw details for callers. Callers must not log those raw values inadvertently.
The diagnostics probe captures SDK logs with a canary in transport/enum errors.
