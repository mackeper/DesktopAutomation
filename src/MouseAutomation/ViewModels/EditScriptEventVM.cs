using CommunityToolkit.Mvvm.ComponentModel;

namespace MouseAutomation.src.MouseAutomation.ViewModels;

public partial class EditScriptEventVM : ObservableObject
{
    [ObservableProperty]
    private bool isVisible = true;

    public EditScriptEventVM()
    {
    }

    public void Open()
    {
        IsVisible = true;
    }

    public void Close()
    {
        IsVisible = false;
    }

}
