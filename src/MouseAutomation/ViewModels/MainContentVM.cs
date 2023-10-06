using CommunityToolkit.Mvvm.ComponentModel;
using MouseAutomation.src.MouseAutomation.ViewModels;

namespace MouseAutomation.ViewModels;
internal partial class MainContentVM : ObservableObject
{
    [ObservableProperty]
    private bool isVisible = true;

    [ObservableProperty]
    private bool isEditScriptEventVisible = false;

    public MainContentVM()
    {
        RecorderVM = new RecorderVM();
        AutoClickerVM = new AutoClickerVM();
    }

    public MainContentVM(
        RecorderVM recorderVM,
        AutoClickerVM autoClickerVM,
        EditScriptEventVM editScriptEventVM)
    {
        RecorderVM = recorderVM;
        AutoClickerVM = autoClickerVM;
        EditScriptEventVM = editScriptEventVM;

        EditScriptEventVM.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == nameof(EditScriptEventVM.IsVisible))
                IsEditScriptEventVisible = EditScriptEventVM.IsVisible;
        };
    }

    public RecorderVM RecorderVM { get; }
    public AutoClickerVM AutoClickerVM { get; }
    public EditScriptEventVM EditScriptEventVM { get; }
}
