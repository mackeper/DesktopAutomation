using FriendlyWin32.Models.Enums;

namespace FriendlyWin32.Interfaces;

public interface IKeyboard : IDisposable
{
    public IDisposable Subscribe<TMessage>(Action<TMessage> handler);

    public void KeyDown(int key);

    public void KeyUp(int key);

    public void Press(int key);
    bool IsKeyDown(int key);
    IEnumerable<int> GetCurrentModifiers();
}
