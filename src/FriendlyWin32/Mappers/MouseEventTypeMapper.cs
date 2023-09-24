using FriendlyWin32.Models.Enums;

namespace FriendlyWin32.Mappers;
internal class MouseEventTypeMapper : IMapper<WindowsMessage, MouseEventType>
{
    public MouseEventType Map(WindowsMessage source)
        => source switch
        {
            WindowsMessage.WM_MOUSEMOVE => MouseEventType.MouseMove,
            WindowsMessage.WM_LBUTTONDOWN => MouseEventType.LeftButtonDown,
            WindowsMessage.WM_LBUTTONUP => MouseEventType.LeftButtonUp,
            WindowsMessage.WM_LBUTTONDBLCLK => MouseEventType.LeftButtonDoubleClick,
            WindowsMessage.WM_RBUTTONDOWN => MouseEventType.RightButtonDown,
            WindowsMessage.WM_RBUTTONUP => MouseEventType.RightButtonUp,
            WindowsMessage.WM_RBUTTONDBLCLK => MouseEventType.RightButtonDoubleClick,
            WindowsMessage.WM_MBUTTONDOWN => MouseEventType.MiddleButtonDown,
            WindowsMessage.WM_MBUTTONUP => MouseEventType.MiddleButtonUp,
            WindowsMessage.WM_MBUTTONDBLCLK => MouseEventType.MiddleButtonDoubleClick,
            WindowsMessage.WM_MOUSEWHEEL => MouseEventType.MouseWheel,
            WindowsMessage.WM_XBUTTONDOWN => MouseEventType.XButtonDown,
            WindowsMessage.WM_XBUTTONUP => MouseEventType.XButtonUp,
            WindowsMessage.WM_XBUTTONDBLCLK => MouseEventType.XButtonDoubleClick,
            WindowsMessage.WM_MOUSEHWHEEL => MouseEventType.MouseHorizontalWheel,
            _ => throw new NotImplementedException(),
        };
}
