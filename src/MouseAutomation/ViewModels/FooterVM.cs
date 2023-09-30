using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FriendlyWin32.Interfaces;
using FriendlyWin32.Models.MouseEvents;
using System.Diagnostics;

namespace MouseAutomation.ViewModels;
internal sealed partial class FooterVM : ObservableObject
{
    private readonly IMouse mouse = null!;

    [ObservableProperty]
    public string version = "Some version";

    [ObservableProperty]
    private MouseMoveEvent mouseInfo = new(1234, 1234);

    [ObservableProperty]
    private bool showSettings = false;

    public FooterVM()
    {
    }

    public FooterVM(string version, IMouse mouse)
    {
        Version = version;
        this.mouse = mouse;
        mouse.Subscribe<MouseMoveEvent>(mouseMoveEvent => MouseInfo = mouseMoveEvent);
    }

    [RelayCommand]
    public void Bug() => Process.Start(new ProcessStartInfo("https://github.com/mackeper/DesktopAutomation/issues/new") { UseShellExecute = true });

    [RelayCommand]
    public void Website() => Process.Start(new ProcessStartInfo("https://github.com/mackeper/DesktopAutomation") { UseShellExecute = true });

    [RelayCommand]
    public void Settings() => ShowSettings = !ShowSettings;


}
