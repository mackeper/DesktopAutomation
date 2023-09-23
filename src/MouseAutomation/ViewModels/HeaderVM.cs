using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace MouseAutomation.ViewModels;
internal partial class HeaderVM : ObservableObject
{
    private readonly Action closeAction;
    private readonly Action minimizeAction;
    [ObservableProperty]
    private string title = "Mouse Automation";

    public HeaderVM()
    {
        closeAction = () => { };
        minimizeAction = () => { };
    }

    public HeaderVM(Action closeAction, Action minimizeAction)
    {
        this.closeAction = closeAction;
        this.minimizeAction = minimizeAction;
    }

    public void CloseCommand() => closeAction();

    public void MinimizeCommand() => minimizeAction();

}
