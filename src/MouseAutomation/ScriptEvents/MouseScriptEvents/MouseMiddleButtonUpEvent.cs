using FriendlyWin32.Interfaces;
using System;

namespace MouseAutomation.ScriptEvents.MouseScriptEvents;

internal class MouseMiddleButtonUpEvent : MouseScriptEvent
{
    private readonly IMouse mouse;

    public MouseMiddleButtonUpEvent(IMouse mouse, int id, TimeSpan delay, int x, int y)
        : base(id, delay, x, y, "Middle up")
    {
        this.mouse = mouse;
    }

    protected override void ExecuteEvent() => mouse.MiddleButtonUp(X, Y);
}
