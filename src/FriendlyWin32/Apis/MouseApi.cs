using System.Runtime.InteropServices;
using Win32.Hooks;

namespace win32.Apis;

internal static partial class MouseApi
{
    [Flags]
    public enum MouseEventFlags
    {
        LeftDown = 0x00000002,
        LeftUp = 0x00000004,
        MiddleDown = 0x00000020,
        MiddleUp = 0x00000040,
        Move = 0x00000001,
        Absolute = 0x00008000,
        RightDown = 0x00000008,
        RightUp = 0x00000010,
        MOUSEEVENTF_WHEEL = 0x0800,
        MOUSEEVENTF_XDOWN = 0x0080,
        MOUSEEVENTF_XUP = 0x0100,
        MOUSEEVENTF_HWHEEL = 0x01000,
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
    public static bool GetMousePosition() => GetCursorPos(out var mousePoint);

    public static void LeftButtonDown(int x, int y) => mouse_event((int)MouseEventFlags.LeftDown, x, y, 0, 0);
    public static void LeftButtonUp(int x, int y) => mouse_event((int)MouseEventFlags.LeftDown, x, y, 0, 0);
    public static void Move(int x, int y) => mouse_event((int)MouseEventFlags.Move, x, y, 0, 0);
    public static void MoveTo(int x, int y) => SetMousePosition(x, y);
}
