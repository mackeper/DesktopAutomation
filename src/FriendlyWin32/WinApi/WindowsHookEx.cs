using FriendlyWin32.Infrastructure;
using FriendlyWin32.Mappers;
using FriendlyWin32.Models;
using FriendlyWin32.Models.Enums;
using FriendlyWin32.Models.KeyboardEvents;
using FriendlyWin32.Models.MouseEvents;
using Serilog;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace FriendlyWin32.WinApi;
internal static class WindowsHookEx
{
    private static readonly Dictionary<WindowsHook, nint> hookIds = new();
    private static readonly MessageBus messageBus = new();
    private static readonly MouseEventTypeMapper mouseEventTypeMapper = new();
    private static readonly KeyboardEventTypeMapper keyboardEventTypeMapper = new();

    private delegate nint LowLevelMouseProc(int nCode, nuint wParam, nint lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true, CallingConvention = CallingConvention.StdCall)]
    private static extern nint SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, nint hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true, CallingConvention = CallingConvention.StdCall)]
    //[return: MarshalAs(UnmanagedType.Bool)]
    private static extern int UnhookWindowsHookEx(nint hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true, CallingConvention = CallingConvention.StdCall)]
    private static extern nint CallNextHookEx(nint hhk, int nCode, nuint wParam, nint lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern nint GetModuleHandle(string lpModuleName);

    [StructLayout(LayoutKind.Sequential)]
    private struct MSLLHOOKSTRUCT
    {
        public Point pt;
        public int mouseData;
        public int flags;
        public int time;
        public nuint dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct Point
    {
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KBDLLHOOKSTRUCT
    {
        public uint vkCode;
        public uint scanCode;
        public uint flags;
        public uint time;
        public nuint dwExtraInfo;
    }

    private static void SetHook(WindowsHook windowsHook, LowLevelMouseProc proc)
    {
        if (hookIds.ContainsKey(windowsHook))
            return;

        //using var currentProcess = Process.GetCurrentProcess();
        //using var currentModule = currentProcess.MainModule!;
        //var p = GetModuleHandle(currentModule!.ModuleName);
        var p = Process.GetCurrentProcess().MainModule!.BaseAddress;
        hookIds[windowsHook] = SetWindowsHookEx((int)windowsHook, proc, p, 0);
    }

    private static nint HookCallback(int nCode, nuint wParam, nint lParam, WindowsHook windowsHook, Action<nuint, nint> handler)
    {
        if (0 >= nCode)
            handler(wParam, lParam);
        return CallNextHookEx(hookIds[windowsHook], nCode, wParam, lParam);
    }

    private static void HandleMouseEvent(nuint wParam, nint lParam)
    {
        var windowsMessage = (WindowsMessage)wParam;
        if (!Enum.IsDefined(windowsMessage))
            return;

        var mouseInfo = Marshal.PtrToStructure<MSLLHOOKSTRUCT>(lParam);
        Log.Debug("{0} ({1}, {2}) (0x{3}, 0x{4})", windowsMessage, mouseInfo.pt.X, mouseInfo.pt.Y, mouseInfo.pt.X.ToString("X"), mouseInfo.pt.Y.ToString("X"));

        var mousePosition = new MousePosition(mouseInfo.pt.X, mouseInfo.pt.Y);
        var mouseEventType = mouseEventTypeMapper.Map(windowsMessage);

        _ = mouseEventType switch
        {
            MouseEventType.MouseMove => messageBus.Publish(new MouseMoveEvent(mousePosition.X, mousePosition.Y)),
            MouseEventType.LeftButtonDown => messageBus.Publish(new LeftButtonDownEvent(mousePosition.X, mousePosition.Y)),
            MouseEventType.LeftButtonUp => messageBus.Publish(new LeftButtonUpEvent(mousePosition.X, mousePosition.Y)),
            MouseEventType.LeftButtonDoubleClick => messageBus.Publish(new LeftButtonDoubleClickEvent(mousePosition.X, mousePosition.Y)),
            MouseEventType.RightButtonDown => messageBus.Publish(new RightButtonDownEvent(mousePosition.X, mousePosition.Y)),
            MouseEventType.RightButtonUp => messageBus.Publish(new RightButtonUpEvent(mousePosition.X, mousePosition.Y)),
            MouseEventType.RightButtonDoubleClick => messageBus.Publish(new RightButtonDoubleClickEvent(mousePosition.X, mousePosition.Y)),
            MouseEventType.MiddleButtonDown => messageBus.Publish(new MiddleButtonDownEvent(mousePosition.X, mousePosition.Y)),
            MouseEventType.MiddleButtonUp => messageBus.Publish(new MiddleButtonUpEvent(mousePosition.X, mousePosition.Y)),
            MouseEventType.MiddleButtonDoubleClick => messageBus.Publish(new MiddleButtonDoubleClickEvent(mousePosition.X, mousePosition.Y)),
            MouseEventType.MouseWheel => messageBus.Publish(new MouseWheelEvent(mouseInfo.mouseData >> 16)),
            MouseEventType.XButtonDown => messageBus.Publish(new XButtonDownEvent(mouseInfo.mouseData >> 16)),
            MouseEventType.XButtonUp => messageBus.Publish(new XButtonUpEvent(mouseInfo.mouseData >> 16)),
            MouseEventType.XButtonDoubleClick => messageBus.Publish(new XButtonDoubleClickEvent(mouseInfo.mouseData >> 16)),
            MouseEventType.MouseHorizontalWheel => messageBus.Publish(new MouseHorizontalWheelEvent(mouseInfo.mouseData >> 16)),
            _ => false
        };

    }

    private static void HandleKeyboardEvent(nuint wParam, nint lParam)
    {
        var windowsMessage = (WindowsMessage)wParam;
        if (!Enum.IsDefined(typeof(WindowsMessage), windowsMessage))
            return;

        var keyInfo = Marshal.PtrToStructure<KBDLLHOOKSTRUCT>(lParam);
        var keyEventType = keyboardEventTypeMapper.Map(windowsMessage);

        _ = keyEventType switch
        {
            KeyboardEventType.KeyDown => messageBus.Publish(new KeyDownEvent((int)keyInfo.vkCode)),
            KeyboardEventType.KeyUp => messageBus.Publish(new KeyUpEvent((int)keyInfo.vkCode)),
            KeyboardEventType.SystemKeyDown => messageBus.Publish(new SystemKeyDownEvent((int)keyInfo.vkCode)),
            KeyboardEventType.SystemKeyUp => messageBus.Publish(new SystemKeyUpEvent((int)keyInfo.vkCode)),
            _ => false
        };
    }

    public static IDisposable Subscribe<TMessage>(Action<TMessage> action)
    {
        Start();
        return messageBus.Subscribe(action);
    }

    private static void Start()
    {
        //var handler = (Action<nint, nint> handler) => (LowLevelMouseProc)((nCode, wParam, lParam) => HookCallback(nCode, wParam, lParam, handler)); // TODO: fix this?
        SetHook(WindowsHook.WH_MOUSE_LL, (nCode, wParam, lParam) => HookCallback(nCode, wParam, lParam, WindowsHook.WH_MOUSE_LL, HandleMouseEvent));
        SetHook(WindowsHook.WH_KEYBOARD_LL, (nCode, wParam, lParam) => HookCallback(nCode, wParam, lParam, WindowsHook.WH_KEYBOARD_LL, HandleKeyboardEvent));
    }

    public static void Stop()
    {
        foreach (var hookId in hookIds.Values)
            UnhookWindowsHookEx(hookId);

        hookIds.Clear();
    }
}
