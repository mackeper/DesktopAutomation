using Core.Model;
using System;

namespace MouseAutomation.ScriptEvents;
internal class FunctionScriptEvent : ScriptEvent
{
    public FunctionScriptEvent(int id, TimeSpan delay) : base(id, delay)
    {
    }

    public override string Name => "Function";

    public override string Icon => "\ue941";

    public override string ExtraInfo => "";

    public override void SetKey(int key) { /* Do nothing */ }

    public override void SetX(int x) { /* Do nothing */ }

    public override void SetY(int y) { /* Do nothing */ }

    protected override void ExecuteEvent() { /* Do nothing */ }
}