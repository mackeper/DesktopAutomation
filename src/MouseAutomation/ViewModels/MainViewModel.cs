using CommunityToolkit.Mvvm.ComponentModel;
using MouseAutomation.Controls;
using Serilog;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Win32.Models.Enums;

namespace MouseAutomation.ViewModels;

internal partial class MainViewModel : ObservableObject
{
    private readonly ILogger log;
    private readonly IRecorder recorder;
    private readonly IPlayer player;
    [ObservableProperty]
    private string title = "Mouse Automation";

    public bool IsRecording => recorder.IsRecording;

    [ObservableProperty]
    public bool isPlaying;

    [ObservableProperty]
    private ObservableCollection<RecordStep> recording = new();

    public MainViewModel()
    {
        // Just for axaml preview
        log = null!;
        recorder = null!;
        player = null!;
        recording.Add(new RecordStep(MouseEventType.LeftButtonDown, 5, 15));
        recording.Add(new RecordStep(MouseEventType.LeftButtonDown, 5, 15));
        recording.Add(new RecordStep(MouseEventType.LeftButtonDown, 5, 15));
    }

    public MainViewModel(ILogger log, IRecorder recorder, IPlayer player)
    {
        this.log = log;
        this.recorder = recorder;
        this.player = player;
    }

    public string RecordButtonText => IsRecording ? "Stop" : "Record";

    public void RecordCommand()
    {

        if (IsRecording)
            StopRecording();
        else
            StartRecording();

        OnPropertyChanged(nameof(IsRecording));
    }

    private void StopRecording()
    {
        log.Information("Stop recording");
        recorder.Stop();
        recorder.GetRecording().ToList().ForEach(Recording.Add);
    }

    private void StartRecording()
    {
        log.Information("Start recording");
        recorder.Start();
    }

    public void ClearRecordingCommand()
    {
        recorder.Clear();
        Recording.Clear();
    }

    public async Task PlayCommand()
    {

        if (IsPlaying)
        {
            IsPlaying = false;
            player.Stop();
        }
        else
        {
            IsPlaying = true;
            await player.Play(Recording);
            IsPlaying = false;
        }
    }
}
