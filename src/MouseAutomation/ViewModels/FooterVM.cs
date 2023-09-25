using CommunityToolkit.Mvvm.ComponentModel;
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

    public FooterVM()
    {
    }

    public FooterVM(string version, IMouse mouse)
    {
        Version = version;
        this.mouse = mouse;
        mouse.Subscribe<MouseMoveEvent>(mouseMoveEvent => MouseInfo = mouseMoveEvent);
    }

    public void BugCommand() => Process.Start(new ProcessStartInfo("https://github.com/mackeper/DesktopAutomation/issues/new") { UseShellExecute = true });

    public void WebsiteCommand() => Process.Start(new ProcessStartInfo("https://github.com/mackeper/DesktopAutomation") { UseShellExecute = true });

}
