using Core;
using Core.Model;
using FriendlyWin32.Interfaces;
using MouseAutomation.ScriptEvents.KeyboardScriptEvents;
using MouseAutomation.ScriptEvents.MouseScriptEvents;
using System;

namespace MouseAutomation.Mappers;
internal class ScriptEventMapper :
    IMapper<ScriptEvent, ScriptEventDTO>,
    IMapper<ScriptEventDTO, ScriptEvent>
{
    private readonly IMouse mouse;
    private readonly IKeyboard keyboard;

    public ScriptEventMapper(IMouse mouse, IKeyboard keyboard)
    {
        this.mouse = mouse;
        this.keyboard = keyboard;
    }

    public ScriptEventDTO Map(ScriptEvent source)
    {
        var scriptEventDTO = new ScriptEventDTO
        {
            Id = source.Id,
            Name = source.Name,
            ExtraInfo = source.ExtraInfo,
            Delay = source.Delay,
            EventType = source.GetType().Name,
        };

        if (source is MouseScriptEvent mouseScriptEvent)
        {
            scriptEventDTO.X = mouseScriptEvent.X;
            scriptEventDTO.Y = mouseScriptEvent.Y;
        }

        if (source is KeyboardScriptEvent keyboardScriptEvent)
        {
            scriptEventDTO.Key = keyboardScriptEvent.Key;
        }

        return scriptEventDTO;
    }

    public ScriptEvent Map(ScriptEventDTO source) =>
        source.EventType switch
        {
            nameof(KeyDownEvent) => new KeyDownEvent(keyboard, source.Id, source.Delay, source.Key),
            nameof(KeyUpEvent) => new KeyUpEvent(keyboard, source.Id, source.Delay, source.Key),
            nameof(MouseLeftButtonDownEvent) => new MouseLeftButtonDownEvent(mouse, source.Id, source.Delay, source.X, source.Y),
            nameof(MouseLeftButtonUpEvent) => new MouseLeftButtonUpEvent(mouse, source.Id, source.Delay, source.X, source.Y),
            nameof(MouseRightButtonDownEvent) => new MouseRightButtonDownEvent(mouse, source.Id, source.Delay, source.X, source.Y),
            nameof(MouseRightButtonUpEvent) => new MouseRightButtonUpEvent(mouse, source.Id, source.Delay, source.X, source.Y),
            nameof(MouseMiddleButtonDownEvent) => new MouseMiddleButtonDownEvent(mouse, source.Id, source.Delay, source.X, source.Y),
            nameof(MouseMiddleButtonUpEvent) => new MouseMiddleButtonUpEvent(mouse, source.Id, source.Delay, source.X, source.Y),
            _ => throw new NotImplementedException(),
        };
}
