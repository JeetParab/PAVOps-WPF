# Validation record — 2026-10-29

Host available to this chat: Windows 11, win-x64, .NET SDK 10.0.403, runtime 10.0.14, Git 2.56.0.

The initial sandbox `dotnet --info` failed while querying Windows services. Running with approved access returned complete SDK information. NuGet restore with approved access succeeded. WSL is not installed and Docker was not found. These facts do not describe the user's Codex Cloud container.

The initial local chat could not access a signed-in Cloud page. The work subsequently continued in the existing Codex Cloud environment; the direct Cloud validation result is recorded below.

Local Windows build/test results are recorded below. Direct Codex Cloud cross-build results are recorded separately; Windows visual/scaling/deployment acceptance remains pending.

- Windows Release solution build: PASS, 0 warnings and 0 errors.
- SQLite integration test: PASS, 1 test, 0 failed or skipped. It covers persistence across new store instances, repeated initialization, updates to an existing key and invalid-key rejection.

The first restore reported NU1903 for SQLitePCLRaw.lib.e_sqlite3 2.1.11. The scaffold updates EF Core to 10.0.14 and explicitly selects SQLitePCLRaw.bundle_e_sqlite3 3.0.5, resolving SQLite 3.53.4. NuGet audit remains enabled and build warnings are errors.


## Direct Codex Cloud validation

On 2026-10-29, the cloud continuation checked out `phase-1-wpf-foundation` at exact commit `75348a80789298749c5777e6b569b78214348263` in `/workspace/PAVOps-WPF`. The checkout was clean before validation; dependency declarations and lockfiles were unchanged. This result is from the actual Codex Cloud environment, independently of GitHub Actions.

Host: Debian 13, Linux x64; .NET SDK 10.0.403, runtime 10.0.14, MSBuild 18.9.15, Git 2.52.0. The retained SDK was activated with:

```bash
source /workspace/.pavops-tools/activate.sh
bash scripts/setup-cloud.sh
bash scripts/validate-cloud.sh
```

- Setup: PASS, exit 0; full solution locked NuGet restore succeeded.
- Validation: PASS, exit 0; full Release solution build included `PAVops.Desktop` targeting `net10.0-windows`, including WPF XAML compilation. Build: 0 warnings, 0 errors.
- SQLite persistence integration test: 1 passed, 0 failed, 0 skipped, executed on .NET 10.0.14.
- No WPF application launch or Windows visual, scaling, packaging or deployment validation was performed on Linux. Those acceptance checks remain pending with the user on Windows.

Preserved evidence for that exact source commit:

- [Setup log](evidence/cloud-2026-10-29/cloud-setup.log)
- [Full solution build and test log](evidence/cloud-2026-10-29/cloud-validation.log)
- [Test runner TRX](evidence/cloud-2026-10-29/persistence.trx)

The evidence commit changes documentation only; the logs identify the source commit tested before this documentation update. The live Cloud copies also remain in ignored `artifacts/`. No application code, data, or WinUI assets were migrated.
