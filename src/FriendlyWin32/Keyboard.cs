using FriendlyWin32.Apis;
using FriendlyWin32.Interfaces;
using FriendlyWin32.Models.Enums;
using FriendlyWin32.WinApi;

namespace FriendlyWin32;
public class Keyboard : IKeyboard
{
    public void Dispose() => WindowsHookEx.Stop();
    public void KeyDown(Key key) => throw new NotImplementedException();
    public void KeyUp(Key key) => throw new NotImplementedException();
    public void Press(Key key) => throw new NotImplementedException();
    public void Subscribe<TMessage>(Action<TMessage> handler) => KeyboardApi.Subscribe(handler);
}
