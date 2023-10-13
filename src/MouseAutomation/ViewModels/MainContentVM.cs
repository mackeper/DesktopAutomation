using CommunityToolkit.Mvvm.ComponentModel;

namespace MouseAutomation.ViewModels;
internal partial class MainContentVM : ObservableObject
{
    [ObservableProperty]
    private bool isVisible = true;

    [ObservableProperty]
    private bool isEditScriptEventVisible = false;

    public MainContentVM()
    {
        RecorderVM = new();
        AutoClickerVM = new();
        WindowHandlerVM = new();
        EditScriptEventVM = new();
    }

    public MainContentVM(
        RecorderVM recorderVM,
        AutoClickerVM autoClickerVM,
        WindowHandlerVM windowHandlerVM,
        EditScriptEventVM editScriptEventVM)
    {
        RecorderVM = recorderVM;
        AutoClickerVM = autoClickerVM;
        WindowHandlerVM = windowHandlerVM;
        EditScriptEventVM = editScriptEventVM;

        EditScriptEventVM.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == nameof(EditScriptEventVM.IsVisible))
                IsEditScriptEventVisible = EditScriptEventVM.IsVisible;
        };
    }

    public RecorderVM RecorderVM { get; }
    public AutoClickerVM AutoClickerVM { get; }
    public WindowHandlerVM WindowHandlerVM { get; }
    public EditScriptEventVM EditScriptEventVM { get; }
}
