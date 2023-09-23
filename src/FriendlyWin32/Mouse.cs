using win32.Apis;
using Win32.Hooks;
using Win32.Interfaces;
using Win32.Models;

namespace Win32;

public sealed partial class Mouse : IMouse
{
    public void Dispose() => WindowsHookEx.Stop();

    public void Subscribe<TMessage>(Action<TMessage> handler) => MouseApi.Subscribe(handler);

    public void Click()
    {
        MouseApi.LeftButtonDown();
        MouseApi.LeftButtonUp();
    }

    public void Click(MousePosition mousePosition) => Click(mousePosition.X, mousePosition.Y);
    public void Click(int x, int y)
    {
        MouseApi.MoveAbsolute(x, y);
        MouseApi.LeftButtonDown(x, y);
        MouseApi.LeftButtonUp(x, y);
    }

    public void LeftButtonDown() => MouseApi.LeftButtonDown();
    public void LeftButtonDown(int x, int y) => MouseApi.LeftButtonDown(x, y);
    public void LeftButtonUp(int x, int y) => MouseApi.LeftButtonUp(x, y);
    public void RightButtonDown(int x, int y) => MouseApi.RightButtonDown(x, y);
    public void RightButtonUp(int x, int y) => MouseApi.RightButtonUp(x, y);
    public void MiddleButtonDown(int x, int y) => MouseApi.MiddleButtonDown(x, y);
    public void MiddleButtonUp(int x, int y) => MouseApi.MiddleButtonUp(x, y);
    public void WheelDown(int x, int y) => MouseApi.WheelDown(x);
    public void WheelUp(int x, int y) => MouseApi.WheelUp(x);


    public void MoveRelative(MousePosition mousePosition) => MoveRelative(mousePosition.X, mousePosition.Y);
    public void MoveRelative(int x, int y) => MouseApi.MoveRelative(x, y);
    public void MoveAbsolute(int x, int y) => MouseApi.MoveAbsolute(x, y);

    public MousePosition GetPosition() => MouseApi.GetPosition();
}
