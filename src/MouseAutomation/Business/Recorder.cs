using Core.Model;
using FriendlyWin32.Interfaces;
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

    public Recorder(ILogger log, IMouse mouse, IKeyboard keyboard)
    {
        this.log = log;
        this.mouse = mouse;
        this.keyboard = keyboard;
        stopwatch = new Stopwatch();
    }

    private void RegisterSubscriptions()
    {
        RegisterSubscription<LeftButtonDownEvent>(e => recording.Add(new MouseLeftButtonDownEvent(mouse, GetNewId(), GetDelayAndRestartStopwatch(), e.X, e.Y)));
        RegisterSubscription<LeftButtonUpEvent>(e => recording.Add(new MouseLeftButtonUpEvent(mouse, GetNewId(), GetDelayAndRestartStopwatch(), e.X, e.Y)));
        RegisterSubscription<RightButtonDownEvent>(e => recording.Add(new MouseRightButtonDownEvent(mouse, GetNewId(), GetDelayAndRestartStopwatch(), e.X, e.Y)));
        RegisterSubscription<RightButtonUpEvent>(e => recording.Add(new MouseRightButtonUpEvent(mouse, GetNewId(), GetDelayAndRestartStopwatch(), e.X, e.Y)));
        RegisterSubscription<MiddleButtonDownEvent>(e => recording.Add(new MouseMiddleButtonDownEvent(mouse, GetNewId(), GetDelayAndRestartStopwatch(), e.X, e.Y)));
        RegisterSubscription<MiddleButtonUpEvent>(e => recording.Add(new MouseMiddleButtonUpEvent(mouse, GetNewId(), GetDelayAndRestartStopwatch(), e.X, e.Y)));

        RegisterSubscription<KeyboardEvents.KeyDownEvent>(e => recording.Add(new KeyDownEvent(keyboard, GetNewId(), GetDelayAndRestartStopwatch(), e.KeyCode)));
        RegisterSubscription<KeyboardEvents.KeyUpEvent>(e => recording.Add(new KeyUpEvent(keyboard, GetNewId(), GetDelayAndRestartStopwatch(), e.KeyCode)));
    }

    private TimeSpan GetDelayAndRestartStopwatch()
    {
        var delay = stopwatch.Elapsed;
        stopwatch.Restart();
        return delay;
    }

    private void RegisterSubscription<TEvent>(Action<TEvent> action)
    {
        subscriptions.Add(mouse.Subscribe(action));
    }

    private void UnregisterSubscriptions()
    {
        foreach (var subscription in subscriptions)
            subscription.Dispose();
        subscriptions.Clear();
    }

    public bool IsRecording => stopwatch.IsRunning;

    public void Start()
    {
        recording.Clear();
        stopwatch.Start();
        RegisterSubscriptions();
    }

    public IEnumerable<ScriptEvent> Stop()
    {
        UnregisterSubscriptions();
        stopwatch.Stop();
        return recording;
    }

    private int GetNewId() => idCounter++;
}
