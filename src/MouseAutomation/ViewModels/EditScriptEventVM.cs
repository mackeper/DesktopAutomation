using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Model;
using FriendlyWin32.Models;
using MouseAutomation.ScriptEvents.KeyboardScriptEvents;
using MouseAutomation.ScriptEvents.MouseScriptEvents;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MouseAutomation.ViewModels;

public partial class EditScriptEventVM : ObservableObject
{
    [ObservableProperty]
    private bool isVisible = false;

    [ObservableProperty]
    private ScriptEvent[] scriptEvents = Array.Empty<ScriptEvent>();

    [ObservableProperty]
    private string eventType;

    [ObservableProperty]
    private int? x;

    [ObservableProperty]
    private int? y;

    [ObservableProperty]
    private bool isPositionsVisible = true;

    [ObservableProperty]
    private int? eventDelay;

    [ObservableProperty]
    private VirtualKey? selectedVirtualKey;

    [ObservableProperty]
    private bool isVirtualKeyVisible = true;

    public IEnumerable<VirtualKey> AvailableKeys { get; }

    private Action? onSaveCallback = null;

    public EditScriptEventVM()
    {
    }

    public EditScriptEventVM(IEnumerable<VirtualKey> availableKeys)
    {
        AvailableKeys = availableKeys;
    }

    public void Open(Action onSaveCallback, params ScriptEvent[] scriptEvents)
    {
        this.onSaveCallback = onSaveCallback;
        Open(scriptEvents);
    }

    public void Open(params ScriptEvent[] scriptEvents)
    {
        IsVisible = true;
        ScriptEvents = scriptEvents;
        SetAll();
    }

    private void SetAll()
    {
        SetEventType();
        SetXAndY();
        SetKey();
        SetDelay();
    }

    private void ResetAll()
    {
        onSaveCallback = null;
        EventType = "None";
        X = null;
        Y = null;
        SelectedVirtualKey = null;
        EventDelay = null;
        ScriptEvents = Array.Empty<ScriptEvent>();
    }

    private void SetEventType()
    {
        if (!ScriptEvents.Any())
            EventType = "None";
        else if (ScriptEvents.Length == 1)
            EventType = ScriptEvents.First().Name;
        else
            EventType = "Multiple";

    }

    private void SetKey()
    {
        IsVirtualKeyVisible = false;
        SelectedVirtualKey = null;

        var keyboardScriptEvents = ScriptEvents.OfType<KeyboardScriptEvent>().ToList();
        if (!keyboardScriptEvents.Any())
            return;

        IsVirtualKeyVisible = true;
        var HasSingleKeyboardEvent = keyboardScriptEvents.Select(k => k.Key).Distinct().Count() == 1;

        if (HasSingleKeyboardEvent)
            SelectedVirtualKey = AvailableKeys.Single(k => k.Value == keyboardScriptEvents.First().Key);
    }

    private void SetXAndY()
    {
        IsPositionsVisible = false;
        X = null;
        Y = null;

        var mouseScriptEvents = ScriptEvents.OfType<MouseScriptEvent>().ToList();
        if (!mouseScriptEvents.Any())
            return;

        IsPositionsVisible = true;
        X = mouseScriptEvents.Select(m => m.X).Distinct().Count() == 1
            ? mouseScriptEvents.First().X
            : null;
        Y = mouseScriptEvents.Select(m => m.Y).Distinct().Count() == 1
            ? mouseScriptEvents.First().Y
            : null;
    }

    private void SetDelay()
    {
        EventDelay = null;
        if (!ScriptEvents.Any())
            return;

        EventDelay = ScriptEvents.Select(s => s.Delay).Distinct().Count() == 1
            ? (int)ScriptEvents.First().Delay.TotalMilliseconds
            : null;
    }

    [RelayCommand]
    public void Close()
    {
        IsVisible = false;
        ResetAll();
    }

    [RelayCommand]
    public void Save()
    {
        foreach (var scriptEvent in ScriptEvents)
        {
            if (EventDelay.HasValue)
                scriptEvent.Delay = TimeSpan.FromMilliseconds(EventDelay.Value);
            if (X.HasValue)
                scriptEvent.SetX(X.Value);
            if (Y.HasValue)
                scriptEvent.SetY(Y.Value);
            if (SelectedVirtualKey.HasValue)
                scriptEvent.SetKey(SelectedVirtualKey.Value.Value);
        }
        IsVisible = false;
        onSaveCallback?.Invoke();
        ResetAll();
    }
}
