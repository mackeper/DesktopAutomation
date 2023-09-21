using Serilog;
using System.Collections.Generic;
using Win32.Interfaces;
using Win32.Models.Enums;
using Win32.Models.MouseEvents;

namespace MouseAutomation.Controls;
internal class Recorder : IRecorder
{
    private readonly ILogger log;
    private readonly IList<RecordStep> recording;
    private bool isRecording = false;

    public Recorder(ILogger log, IMouse mouse, IList<RecordStep> recording)
    {
        this.log = log;
        this.recording = recording;
        mouse.Subscribe<LeftButtonDownEvent>(e => AddRecordStep(new RecordStep(MouseEventType.LeftButtonDown, e.X, e.Y)));
        mouse.Subscribe<LeftButtonUpEvent>(e => AddRecordStep(new RecordStep(MouseEventType.LeftButtonUp, e.X, e.Y)));
    }

    public void Pause() => throw new System.NotImplementedException();

    public void Play() => throw new System.NotImplementedException();

    public void Clear() => recording.Clear();

    public void Start() => isRecording = true;

    public void Stop() => isRecording = false;

    public bool IsRecording => isRecording;

    private void AddRecordStep(RecordStep recordStep)
    {
        if (isRecording)
        {
            log.Debug("Recorder: Add step {0}.", recordStep);
            recording.Add(recordStep);
        }
    }

    public IEnumerable<RecordStep> GetRecording() => recording;
}
