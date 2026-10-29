using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PAVops.Desktop;

public partial class ShellViewModel : ObservableObject
{
    [ObservableProperty] private string currentPage = "Overview";
    public string[] Pages { get; } = ["Overview", "Activities", "Imports", "Reports", "Settings"];
    [RelayCommand]
    private void Navigate(string page) => CurrentPage = page;
}
