using System.IO;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using PAVops.Core;
using PAVops.Infrastructure;
namespace PAVops.Desktop;
public partial class App : Application
{
    private ServiceProvider? services;
    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        ShutdownMode = ShutdownMode.OnExplicitShutdown;
        try
        {
            var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PAVops");
            services = new ServiceCollection()
                .AddSingleton<IApplicationStore>(new ApplicationStore(Path.Combine(directory, "pavops.db")))
                .AddSingleton<ShellViewModel>().AddSingleton<MainWindow>().BuildServiceProvider();
            await services.GetRequiredService<IApplicationStore>().InitializeAsync();
            MainWindow = services.GetRequiredService<MainWindow>();
            ShutdownMode = ShutdownMode.OnMainWindowClose;
            MainWindow.Show();
        }
        catch (Exception exception)
        {
            MessageBox.Show($"PAVops could not start.\n{exception.Message}", "PAVops", MessageBoxButton.OK, MessageBoxImage.Error);
            Shutdown(1);
        }
    }
    protected override void OnExit(ExitEventArgs e)
    {
        services?.Dispose();
        base.OnExit(e);
    }
}
