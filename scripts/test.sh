#!/usr/bin/env bash
set -euo pipefail
root=$(cd "$(dirname "$0")/.." && pwd)
image=${SDK_DOTNET_IMAGE:-mcr.microsoft.com/dotnet/sdk@sha256:ec9c0a0dc5f60adc2065762050638dd5ed5facfc53716d4580a42d86027c8e80}
framework=${SDK_PROBE_FRAMEWORK:-net8.0}
args=(--rm -e DOTNET_CLI_TELEMETRY_OPTOUT=1 -v "$root:/sdk" -v visibility-dotnet-nuget:/root/.nuget/packages -w /sdk/tests)
docker run "${args[@]}" "$image" dotnet restore --locked-mode -p:ProbeFramework="$framework"
docker run "${args[@]}" --network none "$image" dotnet run --no-restore --configuration Release -p:ProbeFramework="$framework"
