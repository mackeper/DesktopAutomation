using FriendlyWin32.Interfaces;
using System;

namespace MouseAutomation.ScriptEvents.KeyboardScriptEvents;
internal class KeyUpEvent : KeyboardScriptEvent
{
    private readonly IKeyboard keyboard;

    public KeyUpEvent(IKeyboard keyboard, int id, TimeSpan delay, int key) : base(id, delay, key, "Key Up")
    {
        this.keyboard = keyboard;
    }

    protected override void ExecuteEvent() => keyboard.KeyUp(Key);
}
