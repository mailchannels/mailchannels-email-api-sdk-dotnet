#!/usr/bin/env bash
set -euo pipefail
root=$(cd "$(dirname "$0")/.." && pwd)
image=mcr.microsoft.com/dotnet/sdk@sha256:ec9c0a0dc5f60adc2065762050638dd5ed5facfc53716d4580a42d86027c8e80
args=(--rm -e DOTNET_CLI_TELEMETRY_OPTOUT=1 -v "$root:/sdk" -v visibility-dotnet-nuget:/root/.nuget/packages -w /sdk)
docker run "${args[@]}" "$image" dotnet restore src/MailChannels.EmailApi/MailChannels.EmailApi.csproj --locked-mode
docker run "${args[@]}" --network none "$image" dotnet pack src/MailChannels.EmailApi/MailChannels.EmailApi.csproj --no-restore --configuration Release -o /sdk/artifacts
