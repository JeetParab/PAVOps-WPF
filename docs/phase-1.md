# Phase 1: environment and acceptance

## Cloud configuration

Use the existing `PAVops-WPF-Dev` environment for `JeetParab/PAVOps-WPF` with Linux, Git and .NET 10 SDK. No application secrets, Windows App SDK, Visual Studio or database service are needed. Repository visibility was observed as public; the historical plan described a private repository. Visibility was not changed.

In environment setup install .NET 10 if it is absent using Microsoft's official installer:

```bash
curl -fsSL https://dot.net/v1/dotnet-install.sh -o /tmp/pavops-dotnet-install.sh
bash /tmp/pavops-dotnet-install.sh --version 10.0.403 --install-dir "$HOME/.dotnet"
```

Make `$HOME/.dotnet` available on PATH in the environment, not only in an ephemeral setup shell. `global.json` requests the .NET 10 stable SDK family with feature-band roll-forward. Setup and validation scripts also resolve this install directory automatically.

Run setup from the checked-out review branch:

```bash
bash scripts/setup-cloud.sh
```

Setup needs HTTPS access to official .NET downloads and NuGet. Agent-side restore also needs NuGet unless the complete restore cache is retained. Use the actual download hosts reported by the setup logs when configuring the environment network policy; do not add application secrets. Publish/save the environment after setup passes.

## Execute Cloud validation

```bash
bash scripts/validate-cloud.sh
```

Preserve `artifacts/cloud-validation.log` and test TRX results. A nonzero exit is a failed gate. The whole solution, including WPF XAML, must build; a Core-only build does not establish desktop cross-build success. Record the host, SDK, commit, exact failing command and error if anything fails. Do not suppress errors or omit the desktop project to claim a pass.

`EnableWindowsTargeting=true` permits acquiring Windows targeting packs on Linux. Actual compatibility must be established by the above run. WPF cannot execute there. The Ubuntu CI job is a second Linux check, not proof that Codex Cloud itself was validated.

## Windows acceptance

1. Restore in locked mode, build Release and run tests using README commands.
2. Launch the app and confirm the overview shell opens without an error dialog.
3. Use mouse and keyboard to select each navigation item; confirm the heading updates and placeholder scope is clear.
4. Check focus, text contrast, resizing down to the minimum size, and 100%, 150% and 200% scaling.
5. Close and reopen. Confirm clean shutdown and a usable local SQLite file.
6. Record tester, Windows version, commit and screenshots before approving Phase 1.

The database uses `EnsureCreated` for the empty Phase 1 settings schema only. Introduce a deliberate EF migration strategy before changing the schema or storing operational records. Concurrent writes to the same setting are not a supported multi-user workflow yet.

## Sources

- [OpenAI Cloud environments](https://learn.chatgpt.com/docs/environments/cloud-environment)
- [Microsoft NETSDK1100 and Windows targeting](https://learn.microsoft.com/en-us/dotnet/core/tools/sdk-errors/netsdk1100)
- [Microsoft SDK Windows targeting property](https://learn.microsoft.com/en-us/dotnet/core/project-sdk/msbuild-props#enablewindowstargeting)
