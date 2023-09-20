using CommunityToolkit.Mvvm.ComponentModel;
using Serilog;
using System.Collections.ObjectModel;
using Win32.Interfaces;

namespace MouseAutomation.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IMouse mouse;
    private readonly ILogger log;

    [ObservableProperty]
    private string title = "Mouse Automation";

    [ObservableProperty]
    private bool isRecording;

    [ObservableProperty]
    private ObservableCollection<string> items = new();

    public MainViewModel()
    {
        // Just for axaml preview
        mouse = null!;
        log = null!;
        items.Add("Item 1");
        items.Add("Item 2");
        items.Add("Item 3");
    }

    public MainViewModel(ILogger log, IMouse mouse)
    {
        this.mouse = mouse;
        this.log = log;
        items.Add("Item 1");
        items.Add("Item 2");
        items.Add("Item 3");
    }

    public string RecordButtonText => IsRecording ? "Stop" : "Record";

    public void RecordCommand()
    {
        log.Information("Record");

        if (IsRecording)
            StopRecording();
        else
            StartRecording();
    }

    private void StartRecording()
    {
        IsRecording = true;

    }

    private void StopRecording()
    {
        IsRecording = false;

    }

    public void PlayCommand()
    {
        log.Information("Play");
        mouse.MoveTo(100, 100);
        mouse.Click(0, 0);
    }
}
