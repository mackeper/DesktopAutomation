using FriendlyWin32.Interfaces;
using System;

namespace MouseAutomation.ScriptEvents.MouseScriptEvents;

internal class MouseRightButtonDownEvent : MouseScriptEvent
{
    private readonly IMouse mouse;

    public MouseRightButtonDownEvent(IMouse mouse, int id, TimeSpan delay, int x, int y)
        : base(id, delay, x, y, "Right down")
    {
        this.mouse = mouse;
    }

    protected override void ExecuteEvent() => mouse.RightButtonDown(X, Y);
}
