using FriendlyWin32.Apis;
using FriendlyWin32.Interfaces;
using FriendlyWin32.WinApi;

namespace FriendlyWin32;
public class Keyboard : IKeyboard
{
    public void Dispose() => WindowsHookEx.Stop();
    public void KeyDown(int key) => KeyboardApi.KeyDown((ushort)key);
    public void KeyUp(int key) => KeyboardApi.KeyUp((ushort)key);
    public void Press(int key) => throw new NotImplementedException();
    public void Subscribe<TMessage>(Action<TMessage> handler) => KeyboardApi.Subscribe(handler);
}
