using FriendlyWin32.Apis;
using FriendlyWin32.Interfaces;
using FriendlyWin32.Models;
using FriendlyWin32.WinApi;

namespace FriendlyWin32;
public class Keyboard : IKeyboard
{
    public void Dispose() => WindowsHookEx.Stop();

    public void KeyDown(int key) => KeyboardApi.KeyDown((ushort)key);

    public void KeyUp(int key) => KeyboardApi.KeyUp((ushort)key);

    public void Press(int key) => throw new NotImplementedException();

    public IDisposable Subscribe<TMessage>(Action<TMessage> handler) => KeyboardApi.Subscribe(handler);

    public bool IsKeyDown(int key) => KeyboardApi.IsKeyDown((ushort)key);

    public IEnumerable<int> GetCurrentModifiers()
    {
        var modifiers = new List<int>
        {
            VirtualKey.SHIFT.Value,
            VirtualKey.CONTROL.Value,
            //VirtualKey.LSHIFT.Value,
            //VirtualKey.RSHIFT.Value,
            //VirtualKey.LCONTROL.Value,
            //VirtualKey.RCONTROL.Value,
            VirtualKey.LWIN.Value,
            VirtualKey.RWIN.Value,
        };

        return modifiers.Where(IsKeyDown);
    }
}
