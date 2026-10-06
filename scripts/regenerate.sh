#!/usr/bin/env bash
set -euo pipefail
root=$(cd "$(dirname "$0")/.." && pwd)
mkdir -p "$root/.generated"
python "$root/codegen/prepare-spec.py" "$root/.generated/openapi.json"
docker run --rm --network none -v "$root:/sdk" openapitools/openapi-generator-cli:v7.26.0@sha256:a304ddf1e2e5f24f68fa3153568d6174cea4959d09aa8e3db6d526fd0782326d generate -i /sdk/.generated/openapi.json -g csharp -c /sdk/codegen/config.json -t /sdk/codegen/templates -o /sdk/.generated/client
cp -r "$root/.generated/client/src/MailChannels.EmailApi/." "$root/src/MailChannels.EmailApi/"
cp "$root/codegen/custom/"*.cs "$root/src/MailChannels.EmailApi/Client/"
cp -r "$root/.generated/client/docs/." "$root/docs/"
cp "$root/codegen/custom/PACKAGE_README.md" "$root/codegen/custom/LICENSE" "$root/"
