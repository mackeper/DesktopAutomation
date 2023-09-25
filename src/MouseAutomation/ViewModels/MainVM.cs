using CommunityToolkit.Mvvm.ComponentModel;
using Serilog;

namespace MouseAutomation.ViewModels;
internal partial class MainVM : ObservableObject
{
    [ObservableProperty]
    private string title = "Mouse Automation";

    private ILogger log;

    public MainVM()
    {
        log = null!;
        HeaderVM = new HeaderVM();
        FooterVM = new FooterVM();
        ScriptVM = new RecorderVM();
        AutoClickerVM = new AutoClickerVM();
    }

    public MainVM(
        ILogger log,
        HeaderVM headerVM,
        FooterVM footerVM,
        RecorderVM scriptVM,
        AutoClickerVM autoClickerVM)
    {
        this.log = log;
        HeaderVM = headerVM;
        FooterVM = footerVM;
        ScriptVM = scriptVM;
        AutoClickerVM = autoClickerVM;
    }

    public HeaderVM HeaderVM { get; }
    public FooterVM FooterVM { get; }
    public RecorderVM ScriptVM { get; }
    public AutoClickerVM AutoClickerVM { get; }
}
