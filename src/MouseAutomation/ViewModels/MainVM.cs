using CommunityToolkit.Mvvm.ComponentModel;
using Serilog;
using System;
using System.ComponentModel;

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
        MainContentVM = new MainContentVM();
    }

    public MainVM(
        ILogger log,
        HeaderVM headerVM,
        FooterVM footerVM,
        MainContentVM mainContentVM,
        SettingsVM settingsVM)
    {
        this.log = log;
        HeaderVM = headerVM;
        FooterVM = footerVM;
        MainContentVM = mainContentVM;
        SettingsVM = settingsVM;
        settingsVM.PropertyChanged += SettingsVM_PropertyChanged;
    }

    private void SettingsVM_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(SettingsVM.IsVisible))
            MainContentVM.IsVisible = !SettingsVM.IsVisible;
    }

    public HeaderVM HeaderVM { get; }
    public FooterVM FooterVM { get; }
    public MainContentVM MainContentVM { get; }
    public SettingsVM SettingsVM { get; }
}
