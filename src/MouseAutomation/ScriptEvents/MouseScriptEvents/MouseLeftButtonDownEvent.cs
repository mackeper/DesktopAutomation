using FriendlyWin32.Interfaces;
using System;

namespace MouseAutomation.ScriptEvents.MouseScriptEvents;

internal class MouseLeftButtonDownEvent : MouseScriptEvent
{
    private readonly IMouse mouse;

    public MouseLeftButtonDownEvent(IMouse mouse, int id, TimeSpan delay, int x, int y)
        : base(id, delay, x, y, "Left down")
    {
        this.mouse = mouse;
    }

    protected override void ExecuteEvent() => mouse.LeftButtonDown(X, Y);
}
