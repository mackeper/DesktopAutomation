using CommunityToolkit.Mvvm.ComponentModel;
using MouseAutomation.Business;
using System;
using System.Threading.Tasks;

namespace MouseAutomation.ViewModels;
internal partial class AutoClickerVM : ObservableObject
{

    [ObservableProperty]
    private int frequencey = 50;

    [ObservableProperty]
    private int numberOfClicks;

    public AutoClickerVM()
    {
        autoClicker = new AutoClicker(null!, null!);
    }

    public AutoClickerVM(IAutoClicker autoClicker)
    {
        this.autoClicker = autoClicker;
    }

    public IAutoClicker autoClicker;
    public async Task ToggleAutoClickerCommand()
    {
        if (autoClicker.IsRunning)
            autoClicker.Stop();
        else
            await autoClicker.Start(TimeSpan.FromMilliseconds(Frequencey));
    }
}
