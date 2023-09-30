using CommunityToolkit.Mvvm.ComponentModel;
using Serilog;

namespace MouseAutomation.ViewModels;
internal partial class MainVM : ObservableObject
{
    [ObservableProperty]
    private string title = "Mouse Automation";

    [ObservableProperty]
    private bool showSettings = false;

    private ILogger log;

    public MainVM()
    {
        log = null!;
        HeaderVM = new HeaderVM();
        FooterVM = new FooterVM();
        MainContentVM = new MainContentVM();
    }

    public MainVM(
        ILogger log,
        HeaderVM headerVM,
        FooterVM footerVM,
        MainContentVM mainContentVM)
    {
        this.log = log;
        HeaderVM = headerVM;
        FooterVM = footerVM;
        MainContentVM = mainContentVM;

        FooterVM.PropertyChanged += FooterVM_PropertyChanged;
    }

    private void FooterVM_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ShowSettings))
        {
            ShowSettings = FooterVM.ShowSettings;
            MainContentVM.IsVisible = !FooterVM.ShowSettings;
        }
    }

    public HeaderVM HeaderVM { get; }
    public FooterVM FooterVM { get; }
    public MainContentVM MainContentVM { get; }
}
