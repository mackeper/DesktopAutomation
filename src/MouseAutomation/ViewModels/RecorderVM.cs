using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core;
using Core.Model;
using Core.Persistance;
using FriendlyWin32.Models.Enums;
using MouseAutomation.Business;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MouseAutomation.ViewModels;

internal partial class RecorderVM : ObservableObject
{
    private readonly ILogger log;
    private readonly IRecorder recorder;
    private readonly IPlayer player;
    private readonly FilePersistance filePersistance;
    private Maybe<string> currentFilepath = Maybe<string>.None;

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
        filePersistance = null!;
        for (var i = 0; i < 20; i++)
            Recording.Add(new RecordStep(i, MouseEventType.LeftButtonDown, 5, 15, TimeSpan.Zero));
    }

    public RecorderVM(
        ILogger log,
        IRecorder recorder,
        IPlayer player,
        HeaderVM headerVM,
        FooterVM footerVM,
        AutoClickerVM autoClickerVM,
        FilePersistance filePersistance)
    {
        this.log = log;
        this.recorder = recorder;
        this.player = player;
        HeaderVM = headerVM;
        FooterVM = footerVM;
        AutoClickerVM = autoClickerVM;
        this.filePersistance = filePersistance;
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
        OnPropertyChanged(nameof(CanSaveRecordingCommand));
        OnPropertyChanged(nameof(CanLoadRecordingCommand));
    }

    private void UpdateRecording(IEnumerable<RecordStep> recording)
    {
        recording.ToList().ForEach(Recording.Add);
    }

    private void StopRecording()
    {
        log.Information("Stop recording");
        var recording = recorder.Stop();
        UpdateRecording(recording);
    }

    private void StartRecording()
    {
        log.Information("Start recording");
        recorder.Start();
    }

    public bool CanClearRecordingCommand
        => !IsPlaying
        && !IsRecording
        && Recording.Any();

    [RelayCommand]
    public void ClearRecording()
    {
        Recording.Clear();
        OnPropertyChanged(nameof(CanRecordCommand));
        OnPropertyChanged(nameof(CanPlayCommand));
        OnPropertyChanged(nameof(CanClearRecordingCommand));
        OnPropertyChanged(nameof(CanSaveRecordingCommand));
    }

    public bool IsPlaying
    {
        get => isPlaying;
        set
        {
            SetProperty(ref isPlaying, value);
            OnPropertyChanged(nameof(CanPlayCommand));
            OnPropertyChanged(nameof(CanRecordCommand));
            OnPropertyChanged(nameof(CanClearRecordingCommand));
            OnPropertyChanged(nameof(CanSaveRecordingCommand));
            OnPropertyChanged(nameof(CanLoadRecordingCommand));
        }
    }

    public bool CanPlayCommand => !IsRecording && Recording.Any();

    [RelayCommand(IncludeCancelCommand = true)]
    public async Task Play(CancellationToken cancellationToken)
    {
        if (IsPlaying)
            return;

        IsPlaying = true;
        await player.Play(Recording, cancellationToken);
        IsPlaying = false;
    }

    [RelayCommand]
    public void EditRecordStep(int id)
    {
        // Do something to edit
    }

    [RelayCommand]
    public void RemoveRecordStep(int id)
    {
        Recording.Remove(Recording.Single(r => r.Id == id));
    }

    public bool CanSaveRecordingCommand => Recording.Any() && !IsRecording && !IsPlaying;

    [RelayCommand]
    public void SaveRecording()
    {
        currentFilepath.Match(
            async path => await filePersistance.Save(path, new Recording(Recording)),
            () => filePersistance.SaveAs(new Recording(Recording)));
    }

    public bool CanLoadRecordingCommand => !IsRecording && !IsPlaying;

    [RelayCommand]
    public async Task LoadRecording()
    {
        var file = await filePersistance.Open<Recording>();
        Recording.Clear();
        file.Match(
            recording => recording.ToList().ForEach(r => Recording.Add(r)),
            () => currentFilepath = Maybe<string>.None);

        OnPropertyChanged(nameof(CanRecordCommand));
        OnPropertyChanged(nameof(CanPlayCommand));
        OnPropertyChanged(nameof(CanClearRecordingCommand));
        OnPropertyChanged(nameof(CanSaveRecordingCommand));
        OnPropertyChanged(nameof(CanLoadRecordingCommand));
    }
}
