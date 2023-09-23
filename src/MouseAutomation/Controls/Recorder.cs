using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Win32.Interfaces;
using Win32.Models.Enums;
using Win32.Models.MouseEvents;

namespace MouseAutomation.Controls;
internal class Recorder : IRecorder
{
    private readonly ILogger log;
    private readonly IList<RecordStep> recording;
    private readonly Stopwatch stopwatch;

    public Recorder(ILogger log, IMouse mouse, IList<RecordStep> recording)
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


    private void AddRecordStep(MouseEventType mouseEventType, int x, int y)
    {
        if (IsRecording)
        {
            var recordStep = new RecordStep(mouseEventType, x, y, stopwatch.Elapsed);
            stopwatch.Restart();
            log.Debug("Recorder: Add step {0}.", recordStep);
            recording.Add(recordStep);
        }
    }

    public IEnumerable<RecordStep> GetRecording() => recording;
}
