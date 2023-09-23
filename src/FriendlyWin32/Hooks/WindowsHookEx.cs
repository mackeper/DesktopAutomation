using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Win32.Infrastructure;
using Win32.Mappers;
using Win32.Models.Enums;
using Win32.Models.KeyboardEvents;
using Win32.Models.MouseEvents;

namespace Win32.Hooks;
internal static class WindowsHookEx
{
    private static readonly ConcurrentDictionary<WindowsHook, nint> hookIds = new();
    private static readonly MessageBus messageBus = new();
    private static readonly MouseEventTypeMapper mouseEventTypeMapper = new();
    private static readonly KeyboardEventTypeMapper keyboardEventTypeMapper = new();

    private delegate nint LowLevelMouseProc(int nCode, nint wParam, nint lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern nint SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, nint hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(nint hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern nint CallNextHookEx(nint hhk, int nCode, nint wParam, nint lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern nint GetModuleHandle(string lpModuleName);

    [StructLayout(LayoutKind.Sequential)]
    private struct MSLLHOOKSTRUCT
    {
        public Point pt;
        public uint mouseData;
        public uint flags;
        public uint time;
        public nint dwExtraInfo;
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
        public UIntPtr dwExtraInfo;
    }

    private static void SetHook(WindowsHook windowsHook, LowLevelMouseProc proc)
    {
        if (hookIds.ContainsKey(windowsHook))
            return;

        using var currentProcess = Process.GetCurrentProcess();
        using var currentModule = currentProcess.MainModule!;
        hookIds[windowsHook] = SetWindowsHookEx((int)windowsHook, proc, GetModuleHandle(currentModule!.ModuleName), 0);
    }

    private static nint HookCallback(int nCode, nint wParam, nint lParam, Action<nint, nint> handler)
    {
        if (nCode >= 0)
            Task.Run(() => handler(wParam, lParam));
        return CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
    }

    private static void HandleMouseEvent(nint wParam, nint lParam)
    {

        var windowsMessage = (WindowsMessage)wParam;
        if (!Enum.IsDefined(windowsMessage))
            return;

        var mouseInfo = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT))!;
        var mouseEventType = mouseEventTypeMapper.Map(windowsMessage);
        _ = mouseEventType switch
        {
            MouseEventType.MouseMove => messageBus.Publish(new MouseMoveEvent(mouseInfo.pt.X, mouseInfo.pt.Y)),
            MouseEventType.LeftButtonDown => messageBus.Publish(new LeftButtonDownEvent(mouseInfo.pt.X, mouseInfo.pt.Y)),
            MouseEventType.LeftButtonUp => messageBus.Publish(new LeftButtonUpEvent(mouseInfo.pt.X, mouseInfo.pt.Y)),
            MouseEventType.LeftButtonDoubleClick => messageBus.Publish(new LeftButtonDoubleClickEvent(mouseInfo.pt.X, mouseInfo.pt.Y)),
            MouseEventType.RightButtonDown => messageBus.Publish(new RightButtonDownEvent(mouseInfo.pt.X, mouseInfo.pt.Y)),
            MouseEventType.RightButtonUp => messageBus.Publish(new RightButtonUpEvent(mouseInfo.pt.X, mouseInfo.pt.Y)),
            MouseEventType.RightButtonDoubleClick => messageBus.Publish(new RightButtonDoubleClickEvent(mouseInfo.pt.X, mouseInfo.pt.Y)),
            MouseEventType.MiddleButtonDown => messageBus.Publish(new MiddleButtonDownEvent(mouseInfo.pt.X, mouseInfo.pt.Y)),
            MouseEventType.MiddleButtonUp => messageBus.Publish(new MiddleButtonUpEvent(mouseInfo.pt.X, mouseInfo.pt.Y)),
            MouseEventType.MiddleButtonDoubleClick => messageBus.Publish(new MiddleButtonDoubleClickEvent(mouseInfo.pt.X, mouseInfo.pt.Y)),
            MouseEventType.MouseWheel => messageBus.Publish(new MouseWheelEvent((int)(mouseInfo.mouseData >> 16))),
            MouseEventType.XButtonDown => messageBus.Publish(new XButtonDownEvent((int)(mouseInfo.mouseData >> 16))),
            MouseEventType.XButtonUp => messageBus.Publish(new XButtonUpEvent((int)(mouseInfo.mouseData >> 16))),
            MouseEventType.XButtonDoubleClick => messageBus.Publish(new XButtonDoubleClickEvent((int)(mouseInfo.mouseData >> 16))),
            MouseEventType.MouseHorizontalWheel => messageBus.Publish(new MouseHorizontalWheelEvent((int)(mouseInfo.mouseData >> 16))),
            _ => false
        };

    }

    private static void HandleKeyboardEvent(nint wParam, nint lParam)
    {
        var windowsMessage = (WindowsMessage)wParam;
        if (!Enum.IsDefined(typeof(WindowsMessage), windowsMessage))
            return;


        var keyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT))!;
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
        SetHook(WindowsHook.WH_MOUSE_LL, (nCode, wParam, lParam) => HookCallback(nCode, wParam, lParam, HandleMouseEvent));
        SetHook(WindowsHook.WH_KEYBOARD_LL, (nCode, wParam, lParam) => HookCallback(nCode, wParam, lParam, HandleKeyboardEvent));
    }

    public static void Stop()
    {
        foreach (var hookId in hookIds.Values)
            UnhookWindowsHookEx(hookId);

        hookIds.Clear();
    }
}
