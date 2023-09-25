using FriendlyWin32.Interfaces;
using FriendlyWin32.Models.Enums;
using FriendlyWin32.Models.MouseEvents;
using MouseAutomation.Controls;
using MouseAutomation.Controls.Model;
using Serilog;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MouseAutomation.Business;
internal class Recorder : IRecorder
{
    private readonly ILogger log;
    private readonly Recording recording;
    private readonly Stopwatch stopwatch;
    private int idCounter = 0;

    public Recorder(ILogger log, IMouse mouse, Recording recording)
    {
        this.log = log;
        this.recording = recording;

        stopwatch = new Stopwatch();

        mouse.Subscribe<LeftButtonDownEvent>(e => AddRecordStep(MouseEventType.LeftButtonDown, e.X, e.Y));
        mouse.Subscribe<LeftButtonUpEvent>(e => AddRecordStep(MouseEventType.LeftButtonUp, e.X, e.Y));
        mouse.Subscribe<RightButtonDownEvent>(e => AddRecordStep(MouseEventType.RightButtonDown, e.X, e.Y));
        mouse.Subscribe<RightButtonUpEvent>(e => AddRecordStep(MouseEventType.RightButtonUp, e.X, e.Y));
        mouse.Subscribe<MiddleButtonDownEvent>(e => AddRecordStep(MouseEventType.MiddleButtonDown, e.X, e.Y));
        mouse.Subscribe<MiddleButtonUpEvent>(e => AddRecordStep(MouseEventType.MiddleButtonUp, e.X, e.Y));
    }

    public bool IsRecording => stopwatch.IsRunning;

    public void Clear() => recording.Clear();

    public void Start() => stopwatch.Start();

    public void Stop() => stopwatch.Stop();

    public bool Remove(int id) => recording.Remove(recording.Single(recordStep => recordStep.Id == id));

    private int GetNewId() => idCounter++;

    private void AddRecordStep(MouseEventType mouseEventType, int x, int y)
    {
        if (IsRecording)
        {
            var recordStep = new RecordStep(GetNewId(), mouseEventType, x, y, stopwatch.Elapsed);
            stopwatch.Restart();
            log.Debug("Recorder: Add step {0}.", recordStep);
            recording.Add(recordStep);
        }
    }

    public IEnumerable<RecordStep> GetRecording() => recording;
}
