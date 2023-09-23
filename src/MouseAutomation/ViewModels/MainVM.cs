using CommunityToolkit.Mvvm.ComponentModel;
using MouseAutomation.Controls;
using Serilog;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Win32.Models.Enums;

namespace MouseAutomation.ViewModels;

internal partial class MainVM : ObservableObject
{
    private readonly ILogger log;
    private readonly IRecorder recorder;
    private readonly IPlayer player;
    [ObservableProperty]
    private string title = "Mouse Automation";

    public bool IsRecording => recorder.IsRecording;

    public bool isPlaying;

    [ObservableProperty]
    private ObservableCollection<RecordStep> recording = new();

    public MainVM()
    {
        // Just for axaml preview
        log = null!;
        recorder = null!;
        player = null!;
        HeaderVM = new HeaderVM();
        FooterVM = new FooterVM();
        AutoClickerVM = new AutoClickerVM();
        Recording.Add(new RecordStep(MouseEventType.LeftButtonDown, 5, 15, TimeSpan.Zero));
        Recording.Add(new RecordStep(MouseEventType.LeftButtonDown, 5, 15, TimeSpan.Zero));
        Recording.Add(new RecordStep(MouseEventType.LeftButtonDown, 5, 15, TimeSpan.Zero));
    }

    public MainVM(
        ILogger log,
        IRecorder recorder,
        IPlayer player,
        HeaderVM headerVM,
        FooterVM footerVM,
        AutoClickerVM autoClickerVM)
    {
        this.log = log;
        this.recorder = recorder;
        this.player = player;
        HeaderVM = headerVM;
        FooterVM = footerVM;
        AutoClickerVM = autoClickerVM;
    }

    public HeaderVM HeaderVM { get; }
    public FooterVM FooterVM { get; }
    public AutoClickerVM AutoClickerVM { get; }

    public bool IsRecordCommandEnabled => !IsPlaying;
    public void RecordCommand()
    {
        if (IsRecording)
            StopRecording();
        else
            StartRecording();

        OnPropertyChanged(nameof(IsRecording));
        OnPropertyChanged(nameof(IsClearRecordingCommandEnabled));
        OnPropertyChanged(nameof(IsPlayCommandEnabled));
    }

    private void StopRecording()
    {
        log.Information("Stop recording");
        recorder.Stop();
        Recording.Clear();
        recorder.GetRecording().ToList().ForEach(Recording.Add);
    }

    private void StartRecording()
    {
        log.Information("Start recording");
        recorder.Start();
    }

    public bool IsClearRecordingCommandEnabled
        => !IsPlaying
        && !IsRecording
        && recorder.GetRecording().Any();

    public void ClearRecordingCommand()
    {
        recorder.Clear();
        Recording.Clear();
    }

    public bool IsPlaying
    {
        get => isPlaying;
        set
        {
            SetProperty(ref isPlaying, value);
            OnPropertyChanged(nameof(IsRecordCommandEnabled));
            OnPropertyChanged(nameof(IsClearRecordingCommandEnabled));
        }
    }

    public bool IsPlayCommandEnabled
        => !IsRecording
        && recorder.GetRecording().Any();

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
