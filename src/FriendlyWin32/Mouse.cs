using win32.Apis;
using Win32.Hooks;
using Win32.Interfaces;
using Win32.Models;

namespace Win32;

public sealed partial class Mouse : IMouse
{
    public void Dispose() => WindowsHookEx.Stop();

    public void Subscribe<TMessage>(Action<TMessage> handler) => MouseApi.Subscribe(handler);

    public void Click(MousePosition mousePosition) => Click(mousePosition.X, mousePosition.Y);
    public void Click(int x, int y)
    {
        MouseApi.MoveTo(x, y);
        MouseApi.LeftButtonDown(x, y);
    }

    public void Move(MousePosition mousePosition) => Move(mousePosition.X, mousePosition.Y);
    public void Move(int x, int y) => MouseApi.Move(x, y);
    public void MoveTo(int x, int y) => MouseApi.MoveTo(x, y);
}
