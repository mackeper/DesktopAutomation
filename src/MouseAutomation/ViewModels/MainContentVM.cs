using CommunityToolkit.Mvvm.ComponentModel;

namespace MouseAutomation.ViewModels;
internal partial class MainContentVM : ObservableObject
{
    [ObservableProperty]
    private bool isVisible = true;

    public MainContentVM()
    {
        RecorderVM = new RecorderVM();
        AutoClickerVM = new AutoClickerVM();
    }

    public MainContentVM(
        RecorderVM recorderVM,
        AutoClickerVM autoClickerVM)
    {
        RecorderVM = recorderVM;
        AutoClickerVM = autoClickerVM;
    }

    public RecorderVM RecorderVM { get; }
    public AutoClickerVM AutoClickerVM { get; }
}
