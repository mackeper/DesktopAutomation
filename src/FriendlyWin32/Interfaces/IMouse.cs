using Win32.Models;

namespace Win32.Interfaces;
public interface IMouse : IDisposable
{
    public void Subscribe<TMessage>(Action<TMessage> handler);
    void Click(int x, int y);
    void Move(int x, int y);
    void MoveTo(int x, int y);
}
