using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FriendlyWin32.Interfaces;
using FriendlyWin32.Models.MouseEvents;
using System;
using System.Diagnostics;

namespace MouseAutomation.ViewModels;
internal sealed partial class FooterVM : ObservableObject
{
    [ObservableProperty]
    public string version = "Some version";

    [ObservableProperty]
    private MouseMoveEvent mouseInfo = new(1234, 1234);

    private readonly Action showSettings;

    public FooterVM()
    {
    }

    public FooterVM(string version, IMouse mouse, Action showSettings)
    {
        Version = version;
        mouse.Subscribe<MouseMoveEvent>(mouseMoveEvent => MouseInfo = mouseMoveEvent);
        this.showSettings = showSettings;
    }

    [RelayCommand]
    public void Bug() => Process.Start(new ProcessStartInfo("https://github.com/mackeper/DesktopAutomation/issues/new") { UseShellExecute = true });

    [RelayCommand]
    public void Website() => Process.Start(new ProcessStartInfo("https://github.com/mackeper/DesktopAutomation") { UseShellExecute = true });

    [RelayCommand]
    public void Settings() => showSettings();


}
