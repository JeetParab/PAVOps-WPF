#!/usr/bin/env bash
set -euo pipefail
cd "$(dirname "$0")/.."
if [ -d "$HOME/.dotnet" ]; then export PATH="$HOME/.dotnet:$PATH"; fi
mkdir -p artifacts
exec > >(tee artifacts/cloud-validation.log) 2>&1
date -u
uname -a
git rev-parse HEAD
dotnet --info
dotnet restore PAVops.slnx --locked-mode
dotnet build PAVops.slnx -c Release --no-restore
dotnet test tests/PAVops.Tests -c Release --no-build --logger trx --results-directory artifacts/tests
echo 'PASS: host build and non-UI tests. Windows UI and deployment acceptance remain separate.'
