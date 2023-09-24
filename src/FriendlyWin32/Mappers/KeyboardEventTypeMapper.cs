using FriendlyWin32.Models.Enums;

namespace FriendlyWin32.Mappers;
internal class KeyboardEventTypeMapper
{
    public KeyboardEventType Map(WindowsMessage source)
    {
        return source switch
        {
            WindowsMessage.WM_KEYDOWN => KeyboardEventType.KeyDown,
            WindowsMessage.WM_KEYUP => KeyboardEventType.KeyUp,
            WindowsMessage.WM_SYSKEYDOWN => KeyboardEventType.SystemKeyDown,
            WindowsMessage.WM_SYSKEYUP => KeyboardEventType.SystemKeyUp,
            _ => throw new NotImplementedException(),
        };
    }
}
