using System.Runtime.InteropServices;

namespace FriendlyWin32.WinApi;
internal static class DeviceContext
{
    [DllImport("User32.dll")]
    public static extern nint GetDC(nint hwnd);

    [DllImport("User32.dll")]
    public static extern void ReleaseDC(nint hwnd, nint dc);
}
