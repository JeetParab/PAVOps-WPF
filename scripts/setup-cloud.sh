#!/usr/bin/env bash
set -euo pipefail
cd "$(dirname "$0")/.."
if [ -d "$HOME/.dotnet" ]; then export PATH="$HOME/.dotnet:$PATH"; fi
command -v dotnet >/dev/null || { echo 'Install the .NET 10 SDK in environment setup before running this script.' >&2; exit 1; }
dotnet --version | grep -Eq '^10\.' || { echo '.NET 10 SDK required' >&2; exit 1; }
dotnet --info
git --version
dotnet restore PAVops.slnx --locked-mode
