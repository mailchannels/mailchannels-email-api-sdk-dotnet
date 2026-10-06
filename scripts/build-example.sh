#!/usr/bin/env bash
set -euo pipefail
root=$(cd "$(dirname "$0")/.." && pwd)
image=mcr.microsoft.com/dotnet/sdk@sha256:e70cdb7f80b0348f5cb85f19a8f670fca061f033d57eed12fa003d58b0e06317
args=(--rm -e DOTNET_CLI_TELEMETRY_OPTOUT=1 -v "$root:/sdk" -v visibility-dotnet-nuget:/root/.nuget/packages -w /sdk)
docker run "${args[@]}" "$image" dotnet restore examples/DryRun/DryRun.csproj --locked-mode
docker run "${args[@]}" --network none "$image" dotnet build examples/DryRun/DryRun.csproj --no-restore --configuration Release
