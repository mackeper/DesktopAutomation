using FriendlyWin32.Interfaces;
using System;

namespace MouseAutomation.ScriptEvents.MouseScriptEvents;

internal class MouseLeftButtonUpEvent : MouseScriptEvent
{
    private readonly IMouse mouse;

    public MouseLeftButtonUpEvent(IMouse mouse, int id, TimeSpan delay, int x, int y)
        : base(id, delay, x, y, "Left up")
    {
        this.mouse = mouse;
    }

    protected override void ExecuteEvent() => mouse.LeftButtonUp(X, Y);
}
