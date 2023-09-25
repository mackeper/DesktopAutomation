using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Model;
using FriendlyWin32.Models.Enums;
using MouseAutomation.Business;
using Serilog;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MouseAutomation.ViewModels;

internal partial class RecorderVM : ObservableObject
{
    private readonly ILogger log;
    private readonly IRecorder recorder;
    private readonly IPlayer player;

    public bool IsRecording => recorder.IsRecording;

    public bool isPlaying;

    [ObservableProperty]
    public string something = "Something blah";

    [ObservableProperty]
    private ObservableCollection<RecordStep> recording = new();

    public RecorderVM()
    {
        // Just for axaml preview
        log = null!;
        recorder = null!;
        player = null!;
        HeaderVM = new HeaderVM();
        FooterVM = new FooterVM();
        AutoClickerVM = new AutoClickerVM();
        for (var i = 0; i < 20; i++)
            Recording.Add(new RecordStep(i, MouseEventType.LeftButtonDown, 5, 15, TimeSpan.Zero));
    }

    public RecorderVM(
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

    public bool CanRecordCommand => !IsPlaying;

    [RelayCommand]
    private void Record()
    {
        if (IsRecording)
            StopRecording();
        else
            StartRecording();

        OnPropertyChanged(nameof(IsRecording));
        OnPropertyChanged(nameof(CanClearRecordingCommand));
        OnPropertyChanged(nameof(CanPlayCommand));
    }

    private void UpdateRecording()
    {
        Recording.Clear();
        recorder.GetRecording().ToList().ForEach(Recording.Add);
    }

    private void StopRecording()
    {
        log.Information("Stop recording");
        recorder.Stop();
        UpdateRecording();
    }

    private void StartRecording()
    {
        log.Information("Start recording");
        recorder.Start();
    }

    public bool CanClearRecordingCommand
        => !IsPlaying
        && !IsRecording
        && recorder.GetRecording().Any();

    [RelayCommand]
    public void ClearRecording()
    {
        recorder.Clear();
        Recording.Clear();
        OnPropertyChanged(nameof(CanRecordCommand));
        OnPropertyChanged(nameof(CanPlayCommand));
        OnPropertyChanged(nameof(CanClearRecordingCommand));
    }

    public bool IsPlaying
    {
        get => isPlaying;
        set
        {
            SetProperty(ref isPlaying, value);
            OnPropertyChanged(nameof(CanRecordCommand));
            OnPropertyChanged(nameof(CanClearRecordingCommand));
        }
    }

    public bool CanPlayCommand
        => !IsRecording
        && recorder.GetRecording().Any();

    [RelayCommand]
    public async Task Play()
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

    [RelayCommand]
    public void EditRecordStep(int id)
    {
        // Do something to edit
        UpdateRecording();
    }

    [RelayCommand]
    public void RemoveRecordStep(int id)
    {
        recorder.Remove(id);
        UpdateRecording();
    }
}
