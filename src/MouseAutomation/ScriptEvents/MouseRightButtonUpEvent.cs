using FriendlyWin32.Interfaces;
using System;

namespace MouseAutomation.ScriptEvents;

internal class MouseRightButtonUpEvent : MouseScriptEvent
{
    private readonly IMouse mouse;

    public MouseRightButtonUpEvent(IMouse mouse, int id, TimeSpan delay, int x, int y)
        : base(id, delay, x, y, "Right up")
    {
        this.mouse = mouse;
    }

    protected override void ExecuteEvent() => mouse.RightButtonUp(X, Y);
}
