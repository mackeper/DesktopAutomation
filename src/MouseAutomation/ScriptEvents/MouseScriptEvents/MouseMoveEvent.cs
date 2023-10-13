using FriendlyWin32.Interfaces;
using System;

namespace MouseAutomation.ScriptEvents.MouseScriptEvents;

internal class MouseMoveEvent : MouseScriptEvent
{
    private readonly IMouse mouse;

    public MouseMoveEvent(IMouse mouse, int id, TimeSpan delay, int x, int y)
        : base(id, delay, x, y, "Mouse move")
    {
        this.mouse = mouse;
    }

    protected override void ExecuteEvent() => mouse.MoveAbsolute(X, Y);
}
