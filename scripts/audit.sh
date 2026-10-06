#!/usr/bin/env bash
set -euo pipefail
root=$(cd "$(dirname "$0")/.." && pwd)
docker run --rm -e DOTNET_CLI_TELEMETRY_OPTOUT=1 -v "$root:/sdk" -v visibility-dotnet-nuget:/root/.nuget/packages -w /sdk mcr.microsoft.com/dotnet/sdk@sha256:ec9c0a0dc5f60adc2065762050638dd5ed5facfc53716d4580a42d86027c8e80 dotnet list src/MailChannels.EmailApi/MailChannels.EmailApi.csproj package --vulnerable --include-transitive --format json
