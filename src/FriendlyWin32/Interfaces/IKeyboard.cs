using FriendlyWin32.Models.Enums;

namespace FriendlyWin32.Interfaces;

public interface IKeyboard : IDisposable
{
    public void Subscribe<TMessage>(Action<TMessage> handler);

    public void KeyDown(Key key);

    public void KeyUp(Key key);

    public void Press(Key key);
}
