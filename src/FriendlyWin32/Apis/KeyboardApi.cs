using FriendlyWin32.WinApi;
using System.Runtime.InteropServices;

namespace FriendlyWin32.Apis;

internal interface KeyboardApi
{
    public static IDisposable Subscribe<TMessage>(Action<TMessage> handler) => WindowsHookEx.Subscribe(handler);

    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-input
    [StructLayout(LayoutKind.Sequential)]
    private struct INPUT
    {
        public uint type;
        public InputUnion U;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct InputUnion
    {
        [FieldOffset(0)]
        public MOUSEINPUT mi;

        [FieldOffset(0)]
        public KEYBDINPUT ki;

        [FieldOffset(0)]
        public HARDWAREINPUT hi;
    }

    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-mouseinput
    [StructLayout(LayoutKind.Sequential)]
    private struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public uint mouseData;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-keybdinput
    [StructLayout(LayoutKind.Sequential)]
    private struct KEYBDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-hardwareinput
    [StructLayout(LayoutKind.Sequential)]
    private struct HARDWAREINPUT
    {
        public uint uMsg;
        public ushort wParamL;
        public ushort wParamH;
    }

    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendinput
    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

    [DllImport("user32.dll")]
    private static extern short GetAsyncKeyState(int vKey);

    private const int INPUT_KEYBOARD = 1;
    private const uint KEYEVENTF_KEYUP = 0x0002;

    public static void KeyDown(ushort key)
    {
        var inputs = new INPUT[1];

        inputs[0] = new INPUT
        {
            type = INPUT_KEYBOARD,
        };

        inputs[0].U.ki = new KEYBDINPUT
        {
            wVk = key,
        };

        _ = SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
    }

    public static void KeyUp(ushort key)
    {
        var inputs = new INPUT[1];

        inputs[0] = new INPUT
        {
            type = INPUT_KEYBOARD,
        };

        inputs[0].U.ki = new KEYBDINPUT
        {
            wVk = key,
            dwFlags = KEYEVENTF_KEYUP,
        };

        _ = SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
    }
}
