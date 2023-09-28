using Core.Model;
using FriendlyWin32.Interfaces;
using FriendlyWin32.Models.Enums;
using FriendlyWin32.Models.MouseEvents;
using Serilog;
using System.Diagnostics;

namespace MouseAutomation.Business;
internal class Recorder : IRecorder
{
    private readonly ILogger log;
    private readonly Recording recording = new();
    private readonly Stopwatch stopwatch;
    private int idCounter = 0;

    public Recorder(ILogger log, IMouse mouse)
    {
        this.log = log;

        stopwatch = new Stopwatch();

        mouse.Subscribe<LeftButtonDownEvent>(e => AddRecordStep(MouseEventType.LeftButtonDown, e.X, e.Y));
        mouse.Subscribe<LeftButtonUpEvent>(e => AddRecordStep(MouseEventType.LeftButtonUp, e.X, e.Y));
        mouse.Subscribe<RightButtonDownEvent>(e => AddRecordStep(MouseEventType.RightButtonDown, e.X, e.Y));
        mouse.Subscribe<RightButtonUpEvent>(e => AddRecordStep(MouseEventType.RightButtonUp, e.X, e.Y));
        mouse.Subscribe<MiddleButtonDownEvent>(e => AddRecordStep(MouseEventType.MiddleButtonDown, e.X, e.Y));
        mouse.Subscribe<MiddleButtonUpEvent>(e => AddRecordStep(MouseEventType.MiddleButtonUp, e.X, e.Y));
    }

    public bool IsRecording => stopwatch.IsRunning;

    public void Start()
    {
        recording.Clear();
        stopwatch.Start();
    }

    public Recording Stop()
    {
        stopwatch.Stop();
        return recording;
    }

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
}
