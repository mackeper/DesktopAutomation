using FriendlyWin32.Interfaces;
using System;

namespace MouseAutomation.ScriptEvents;
internal class KeyDownEvent : KeyboardScriptEvent
{
    private readonly IKeyboard keyboard;

    public KeyDownEvent(IKeyboard keyboard, int id, TimeSpan delay, int key) : base(id, delay, key, "Key Down")
    {
        this.keyboard = keyboard;
    }

    protected override void ExecuteEvent() => keyboard.KeyDown(Key);
}
