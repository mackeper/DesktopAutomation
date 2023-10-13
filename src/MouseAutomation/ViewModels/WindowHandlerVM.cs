using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using FriendlyWin32.Interfaces;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace MouseAutomation.ViewModels;
public partial class WindowHandlerVM : ObservableObject
{
    private readonly CancellationTokenSource cancellationTokenSource = new();

    private readonly IDisplay display;

    [ObservableProperty]
    private ObservableCollection<string> windows = new();

    public WindowHandlerVM()
    {
        // Just for axaml preview
        windows.Add("Window 1");
        windows.Add("Window 2");
    }

    public WindowHandlerVM(IDisplay display)
    {
        this.display = display;
        display.GetOpenWindowsTitles().ForEach(windows.Add);
        //Task.Run(async () => await RefreshWindows(cancellationTokenSource.Token), cancellationTokenSource.Token);
    }

    ~WindowHandlerVM()
    {
        cancellationTokenSource.Cancel();
    }

    public async Task RefreshWindows(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                Windows.Clear();
                display.GetOpenWindowsTitles().ForEach(Windows.Add);
            });
            await Task.Delay(5000); // TODO add cancellation token
        }
    }
}
