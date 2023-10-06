using FriendlyWin32.Interfaces;
using System;

namespace MouseAutomation.ScriptEvents;

internal class MouseMiddleButtonDownEvent : MouseScriptEvent
{
    private readonly IMouse mouse;

    public MouseMiddleButtonDownEvent(IMouse mouse, int id, TimeSpan delay, int x, int y)
        : base(id, delay, x, y, "Middle down")
    {
        this.mouse = mouse;
    }

    protected override void ExecuteEvent() => mouse.MiddleButtonDown(X, Y);
}
