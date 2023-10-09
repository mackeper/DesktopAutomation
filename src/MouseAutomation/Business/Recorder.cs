using Core.Model;
using FriendlyWin32.Interfaces;
using FriendlyWin32.Models.Enums;
using FriendlyWin32.Models.MouseEvents;
using MouseAutomation.ScriptEvents;
using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

using KeyboardEvents = FriendlyWin32.Models.KeyboardEvents;

namespace MouseAutomation.Business;
internal class Recorder : IRecorder
{
    private readonly ILogger log;
    private readonly IMouse mouse;
    private readonly IKeyboard keyboard;
    private readonly List<ScriptEvent> recording = new();
    private readonly Stopwatch stopwatch;
    private int idCounter = 0;
    private ConcurrentBag<IDisposable> subscriptions = new();

    public bool IsMouseRecording { get; set; } = true;
    public bool IsKeyboardRecording { get; set; } = true;

    public Recorder(ILogger log, IMouse mouse, IKeyboard keyboard)
    {
        this.log = log;
        this.mouse = mouse;
        this.keyboard = keyboard;
        stopwatch = new Stopwatch();

        RegisterMouseSubscription<LeftButtonDownEvent>(e => AddMouseEvent(new MouseLeftButtonDownEvent(mouse, GetNewId(), GetDelayAndRestartStopwatch(), e.X, e.Y)));
        RegisterMouseSubscription<LeftButtonUpEvent>(e => AddMouseEvent(new MouseLeftButtonUpEvent(mouse, GetNewId(), GetDelayAndRestartStopwatch(), e.X, e.Y)));
        RegisterMouseSubscription<RightButtonDownEvent>(e => AddMouseEvent(new MouseRightButtonDownEvent(mouse, GetNewId(), GetDelayAndRestartStopwatch(), e.X, e.Y)));
        RegisterMouseSubscription<RightButtonUpEvent>(e => AddMouseEvent(new MouseRightButtonUpEvent(mouse, GetNewId(), GetDelayAndRestartStopwatch(), e.X, e.Y)));
        RegisterMouseSubscription<MiddleButtonDownEvent>(e => AddMouseEvent(new MouseMiddleButtonDownEvent(mouse, GetNewId(), GetDelayAndRestartStopwatch(), e.X, e.Y)));
        RegisterMouseSubscription<MiddleButtonUpEvent>(e => AddMouseEvent(new MouseMiddleButtonUpEvent(mouse, GetNewId(), GetDelayAndRestartStopwatch(), e.X, e.Y)));

        RegisterKeyboardSubscription<KeyboardEvents.KeyDownEvent>(e => AddKeyboardEvent(new KeyDownEvent(keyboard, GetNewId(), GetDelayAndRestartStopwatch(), e.KeyCode)));
        RegisterKeyboardSubscription<KeyboardEvents.KeyUpEvent>(e => AddKeyboardEvent(new KeyUpEvent(keyboard, GetNewId(), GetDelayAndRestartStopwatch(), e.KeyCode)));
    }

    private void RegisterMouseSubscription<TEvent>(Action<TEvent> action)
    {
        subscriptions.Add(mouse.Subscribe(action));
    }

    private void RegisterKeyboardSubscription<TEvent>(Action<TEvent> action)
    {
        subscriptions.Add(keyboard.Subscribe(action));
    }

    private void AddMouseEvent(ScriptEvent scriptEvent)
    {
        if (scriptEvent == null)
            return;
        if (IsMouseRecording && IsRecording)
            recording.Add(scriptEvent);
    }

    private void AddKeyboardEvent(ScriptEvent scriptEvent)
    {
        if (scriptEvent == null)
            return;
        if (IsKeyboardRecording && IsRecording)
            recording.Add(scriptEvent);
    }

    private TimeSpan GetDelayAndRestartStopwatch()
    {
        var delay = stopwatch.Elapsed;
        stopwatch.Restart();
        return delay;
    }

    private void UnregisterSubscriptions(ConcurrentBag<IDisposable> subscriptions)
    {
        foreach (var subscription in subscriptions)
            subscription.Dispose();
        subscriptions.Clear();
    }

    public bool IsRecording { get; set; }

    public void Start()
    {
        recording.Clear();
        stopwatch.Start();
        IsRecording = true;
    }

    public IEnumerable<ScriptEvent> Stop()
    {
        stopwatch.Stop();
        IsRecording = false;
        return recording;
    }

    private int GetNewId() => idCounter++;
}
