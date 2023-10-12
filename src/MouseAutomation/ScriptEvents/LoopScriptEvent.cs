using Core.Model;
using System;

namespace MouseAutomation.ScriptEvents;
internal class LoopScriptEvent : ScriptEvent
{
    public LoopScriptEvent(int id, TimeSpan delay) : base(id, delay)
    {
    }

    public override string Name => "Loop";

    public override string Icon => "\ue5d5";

    public override string ExtraInfo => "10";

    public override void SetKey(int key) { /* Do nothing */ }
    public override void SetX(int x) { /* Do nothing */ }
    public override void SetY(int y) { /* Do nothing */ }
    protected override void ExecuteEvent() { /* Do nothing */ }
}
