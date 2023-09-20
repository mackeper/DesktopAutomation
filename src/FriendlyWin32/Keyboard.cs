using Win32.Apis;
using Win32.Hooks;
using Win32.Interfaces;
using Win32.Models.Enums;

namespace Win32;
public class Keyboard : IKeyboard
{
    public void Dispose() => WindowsHookEx.Stop();
    public void KeyDown(Key key) => throw new NotImplementedException();
    public void KeyUp(Key key) => throw new NotImplementedException();
    public void Press(Key key) => throw new NotImplementedException();
    public void Subscribe<TMessage>(Action<TMessage> handler) => KeyboardApi.Subscribe(handler);
}
