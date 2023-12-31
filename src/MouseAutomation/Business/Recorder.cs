﻿using Core.Model;
using FriendlyWin32.Interfaces;
using FriendlyWin32.Models.MouseEvents;
using MouseAutomation.ScriptEvents.KeyboardScriptEvents;
using MouseAutomation.ScriptEvents.MouseScriptEvents;
using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using KeyboardEvents = FriendlyWin32.Models.KeyboardEvents;
using MouseEvents = MouseAutomation.ScriptEvents.MouseScriptEvents;

namespace MouseAutomation.Business;
internal class Recorder : IRecorder
{
    private readonly ILogger log;
    private readonly IMouse mouse;
    private readonly IKeyboard keyboard;
    private readonly ObservableCollection<ScriptEvent> recording = new();
    private readonly Stopwatch stopwatch;
    private int idCounter = 0;
    private readonly ConcurrentBag<IDisposable> subscriptions = new();

    private readonly List<Action<ScriptEvent>> recordingSubscribers = new();

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

        recording.CollectionChanged += (sender, args) =>
        {
            if (args.NewItems != null)
                foreach (var scriptEvent in args.NewItems.OfType<ScriptEvent>())
                    foreach (var subscriber in recordingSubscribers)
                        subscriber(scriptEvent);
        };
    }

    public void Subscribe(Action<ScriptEvent> action) => recordingSubscribers.Add(action);

    private void RegisterMouseSubscription<TEvent>(Action<TEvent> action)
        => subscriptions.Add(mouse.Subscribe(action));

    private void RegisterKeyboardSubscription<TEvent>(Action<TEvent> action)
        => subscriptions.Add(keyboard.Subscribe(action));

    /// <remarks>
    /// For drag to work, we need to add a MouseMoveEvent before the MouseLeftButtonUpEvent.
    /// </remarks>
    private bool AddMouseMove(ScriptEvent scriptEvent)
    {
        var previousEvent = recording.LastOrDefault();
        if (scriptEvent is not MouseLeftButtonUpEvent mouseLeftButtonUpEvent ||
           previousEvent is not MouseLeftButtonDownEvent mouseLeftButtonDownEvent)
            return false;

        if (mouseLeftButtonDownEvent.X == mouseLeftButtonUpEvent.X && mouseLeftButtonDownEvent.Y == mouseLeftButtonUpEvent.Y)
            return false;

        recording.Add(new MouseEvents.MouseMoveEvent(
            mouse,
            GetNewId(),
            mouseLeftButtonUpEvent.Delay,
            mouseLeftButtonUpEvent.X,
            mouseLeftButtonUpEvent.Y));

        return true;
    }

    private void AddMouseEvent(ScriptEvent scriptEvent)
    {
        if (IsMouseRecording && IsRecording)
        {
            if (AddMouseMove(scriptEvent)) // If we added a MouseMoveEvent, set the delay to a fixed value.
                scriptEvent.Delay = TimeSpan.FromMilliseconds(30);
            recording.Add(scriptEvent);
        }
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

    private void UnregisterSubscriptions()
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

    public int GetNewId() => idCounter++;
}