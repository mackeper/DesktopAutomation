using FriendlyWin32.Models;
using FriendlyWin32.WinApi;
using System.Runtime.InteropServices;

namespace FriendlyWin32.Apis;

internal static partial class MouseApi
{
    [Flags]
    public enum MouseEventFlag : uint
    {
        MOUSEEVENTF_ABSOLUTE = 0x8000,
        MOUSEEVENTF_LEFTDOWN = 0x0002,
        MOUSEEVENTF_LEFTUP = 0x0004,
        MOUSEEVENTF_MIDDLEDOWN = 0x0020,
        MOUSEEVENTF_MIDDLEUP = 0x0040,
        MOUSEEVENTF_MOVE = 0x0001,
        MOUSEEVENTF_RIGHTDOWN = 0x0008,
        MOUSEEVENTF_RIGHTUP = 0x0010,
        MOUSEEVENTF_WHEEL = 0x0800,
        MOUSEEVENTF_XDOWN = 0x0080,
        MOUSEEVENTF_XUP = 0x0100,
        MOUSEEVENTF_HWHEEL = 0x1000
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct MousePoint
    {
        public int X;
        public int Y;

        public MousePoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool SetCursorPos(int x, int y);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool GetCursorPos(out MousePoint lpMousePoint);

    // Note  This function has been superseded. Use SendInput instead.
    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-mouse_event
    [LibraryImport("user32.dll")]
    private static partial void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

    public static IDisposable Subscribe<TMessage>(Action<TMessage> handler) => WindowsHookEx.Subscribe(handler);
    public static bool SetMousePosition(int x, int y) => SetCursorPos(x, y);

    public static MousePosition GetPosition()
    {
        GetCursorPos(out var mousePoint);
        return new MousePosition(mousePoint.X, mousePoint.Y);
    }

    private static void MouseEvent(MouseEventFlag mouseEventFlag, int x, int y)
    {
        //mouse_event((int)MouseEventFlag.MOUSEEVENTF_ABSOLUTE, x, y, 0, 0);
        SetCursorPos(x, y);
        mouse_event((int)mouseEventFlag, 0, 0, 0, 0);
    }

    private static void MouseEvent(MouseEventFlag mouseEventFlag)
    {
        mouse_event((int)mouseEventFlag, 0, 0, 0, 0);
    }

    public static void LeftButtonDown() => MouseEvent(MouseEventFlag.MOUSEEVENTF_LEFTDOWN);
    public static void LeftButtonDown(int x, int y) => MouseEvent(MouseEventFlag.MOUSEEVENTF_LEFTDOWN, x, y);
    public static void LeftButtonUp() => MouseEvent(MouseEventFlag.MOUSEEVENTF_LEFTUP);
    public static void LeftButtonUp(int x, int y) => MouseEvent(MouseEventFlag.MOUSEEVENTF_LEFTUP, x, y);

    public static void RightButtonDown() => MouseEvent(MouseEventFlag.MOUSEEVENTF_RIGHTDOWN);
    public static void RightButtonDown(int x, int y) => MouseEvent(MouseEventFlag.MOUSEEVENTF_RIGHTDOWN, x, y);
    public static void RightButtonUp() => MouseEvent(MouseEventFlag.MOUSEEVENTF_RIGHTUP);
    public static void RightButtonUp(int x, int y) => MouseEvent(MouseEventFlag.MOUSEEVENTF_RIGHTUP, x, y);

    public static void MiddleButtonDown() => MouseEvent(MouseEventFlag.MOUSEEVENTF_MIDDLEDOWN);
    public static void MiddleButtonDown(int x, int y) => MouseEvent(MouseEventFlag.MOUSEEVENTF_MIDDLEDOWN, x, y);
    public static void MiddleButtonUp() => MouseEvent(MouseEventFlag.MOUSEEVENTF_MIDDLEUP);
    public static void MiddleButtonUp(int x, int y) => MouseEvent(MouseEventFlag.MOUSEEVENTF_MIDDLEUP, x, y);

    public static void MoveRelative(int x, int y) => MouseEvent(MouseEventFlag.MOUSEEVENTF_MOVE, x, y);
    public static void MoveAbsolute(int x, int y) => MouseEvent(MouseEventFlag.MOUSEEVENTF_ABSOLUTE, x, y);

    public static void WheelDown(int x) => MouseEvent(MouseEventFlag.MOUSEEVENTF_WHEEL, x, 0);
    public static void WheelUp(int x) => MouseEvent(MouseEventFlag.MOUSEEVENTF_WHEEL, x, 0);
}
