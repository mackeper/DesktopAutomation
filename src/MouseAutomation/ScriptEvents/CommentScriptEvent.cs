using Core.Model;
using System;

namespace MouseAutomation.ScriptEvents;
internal class CommentScriptEvent : ScriptEvent
{
    public CommentScriptEvent(int id) : base(id, TimeSpan.Zero)
    {
    }

    public override string Name => "Comment";

    public override string Icon => "\ue0ca";

    public override string ExtraInfo => "";

    public override void SetKey(int key) { /* Do nothing */ }

    public override void SetX(int x) { /* Do nothing */ }

    public override void SetY(int y) { /* Do nothing */ }

    protected override void ExecuteEvent() { /* Do nothing */ }
}
