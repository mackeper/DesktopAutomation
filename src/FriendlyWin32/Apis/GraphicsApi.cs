using FriendlyWin32.WinApi;
using System.Drawing;

namespace FriendlyWin32.Apis;

internal static class GraphicsApi
{
    public static void Draw()
    {
        IntPtr desktopPtr = DeviceContext.GetDC(IntPtr.Zero);
        var g = Graphics.FromHdc(desktopPtr);

        var b = new SolidBrush(Color.White);
        g.FillRectangle(b, new Rectangle(500, 500, 500, 500));

        g.Dispose();
        DeviceContext.ReleaseDC(IntPtr.Zero, desktopPtr);
    }
}
