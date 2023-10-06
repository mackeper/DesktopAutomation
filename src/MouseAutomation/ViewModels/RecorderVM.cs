using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core;
using Core.Model;
using Core.Persistance;
using FriendlyWin32.Models.Enums;
using MouseAutomation.Business;
using MouseAutomation.ScriptEvents;
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
    private readonly IMapper<ScriptEvent, ScriptEventDTO> scriptEventDTOMapper;
    private readonly IMapper<ScriptEventDTO, ScriptEvent> scriptEventMapper;
    private readonly IFilePersistance filePersistance;
    private Maybe<string> currentFilepath = Maybe<string>.None;

    public bool IsRecording => recorder.IsRecording;

    public bool isPlaying;

    [ObservableProperty]
    public string something = "Something blah";

    [ObservableProperty]
    private ObservableCollection<ScriptEvent> recording = new();

    public RecorderVM()
    {
        // Just for axaml preview
        log = null!;
        recorder = null!;
        player = null!;
        AutoClickerVM = new AutoClickerVM();
        filePersistance = null!;
        for (var i = 0; i < 20; i++)
            Recording.Add(new MouseLeftButtonDownEvent(null!, 0, TimeSpan.Zero, 0, 0));
    }

    public RecorderVM(
        ILogger log,
        IRecorder recorder,
        IPlayer player,
        IMapper<ScriptEvent, ScriptEventDTO> scriptEventDTOMapper,
        IMapper<ScriptEventDTO, ScriptEvent> scriptEventMapper,
        AutoClickerVM autoClickerVM,
        IFilePersistance filePersistance)
    {
        this.log = log;
        this.recorder = recorder;
        this.player = player;
        this.scriptEventDTOMapper = scriptEventDTOMapper;
        this.scriptEventMapper = scriptEventMapper;
        AutoClickerVM = autoClickerVM;
        this.filePersistance = filePersistance;
    }

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

    private void UpdateRecording(IEnumerable<ScriptEvent> recording)
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
        var recordingDTO = Recording.Select(scriptEventDTOMapper.Map);
        currentFilepath.Match(
            async path => await filePersistance.Save(path, recordingDTO),
            () => filePersistance.SaveAs(recordingDTO));
    }

    public bool CanLoadRecordingCommand => !IsRecording && !IsPlaying;

    [RelayCommand]
    public async Task LoadRecording()
    {
        var file = await filePersistance.Open<IEnumerable<ScriptEventDTO>>();
        Recording.Clear();
        file.Match(
            recording => recording.ToList().ForEach(r => Recording.Add(scriptEventMapper.Map(r))),
            () => currentFilepath = Maybe<string>.None);

        OnPropertyChanged(nameof(CanRecordCommand));
        OnPropertyChanged(nameof(CanPlayCommand));
        OnPropertyChanged(nameof(CanClearRecordingCommand));
        OnPropertyChanged(nameof(CanSaveRecordingCommand));
        OnPropertyChanged(nameof(CanLoadRecordingCommand));
    }
}
