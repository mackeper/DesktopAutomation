using Core.Model;
using FriendlyWin32.Models;
using System;

namespace MouseAutomation.ScriptEvents.KeyboardScriptEvents;
public abstract class KeyboardScriptEvent : ScriptEvent
{
    public int Key { get; private set; }

    public override string Name { get; }

    public override string ExtraInfo
    {
        get
        {
            try
            {
                return VirtualKey.FromKeyCode(Key).ShortName;
            }
            catch (Exception)
            {
                return Key.ToString();
            }
        }
    }

    public override string Icon => "\uE312";

    protected KeyboardScriptEvent(int id, TimeSpan delay, int key, string name) : base(id, delay)
    {
        Key = key;
        Name = name;
    }

    public override void SetX(int x) { /* Do nothing */ }

    public override void SetY(int y) { /* Do nothing */ }

    public override void SetKey(int key) => Key = key;
}
