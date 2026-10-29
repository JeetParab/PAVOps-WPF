# PAVops WPF

Fresh Phase 1 foundation: C# / .NET 10 / WPF / MVVM / EF Core SQLite. No WinUI source, assets, database or project files were reused.

## Build and run

Install the .NET 10 SDK, then run from the repository root:

```powershell
dotnet restore PAVops.slnx --locked-mode
dotnet build PAVops.slnx -c Release --no-restore
dotnet test tests/PAVops.Tests -c Release --no-build
dotnet run --project src/PAVops.Desktop -c Release --no-build
```

The final command requires Windows. The desktop stores its fresh database in `%LOCALAPPDATA%/PAVops/pavops.db`. Startup initializes SQLite before showing the shell; failure displays an error and exits with code 1.

## Layout

- `PAVops.Core`: platform independent application contracts.
- `PAVops.Infrastructure`: EF Core SQLite settings store with short-lived contexts.
- `PAVops.Desktop`: WPF composition root, DI, MVVM navigation and shared theme resources.
- `PAVops.Tests`: real SQLite persistence, reopening, idempotent initialization and update checks.
- `scripts`: Cloud setup and validation.
- `.github/workflows/validate.yml`: independent Linux and Windows restore/build/test jobs.

Navigation exposes foundation placeholders only. Activity tracking, engineer workflows, Excel imports, reports and PAV integration are outside Phase 1.

Read [Cloud setup and acceptance](docs/phase-1.md) and [validation evidence](docs/validation.md). Phase 1 remains open until Cloud validation and Windows UI acceptance are recorded.
