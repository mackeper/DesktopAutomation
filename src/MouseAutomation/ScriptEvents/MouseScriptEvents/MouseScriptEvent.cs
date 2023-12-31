﻿using Core.Model;
using System;

namespace MouseAutomation.ScriptEvents.MouseScriptEvents;
public abstract class MouseScriptEvent : ScriptEvent
{
    public int X { get; private set; }
    public int Y { get; private set; }

    protected MousePosition Position => new(X, Y);

    public override string Name { get; }

    public override string ExtraInfo => Position.ToString();

    public override string Icon => "\uE323";

    protected MouseScriptEvent(int id, TimeSpan delay, int x, int y, string name) : base(id, delay)
    {
        X = x;
        Y = y;
        Name = name;
    }

    public override void SetX(int x) => X = x;

    public override void SetY(int y) => Y = y;

    public override void SetKey(int key) { /* Do nothing */ }
}
