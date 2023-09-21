using Win32.Models;

namespace Win32.Interfaces;
public interface IMouse : IDisposable
{
    public void Subscribe<TMessage>(Action<TMessage> handler);
    void Click(int x, int y);
    void Move(int x, int y);
    void MoveTo(int x, int y);
    void LeftButtonDown(int x, int y);
    void LeftButtonUp(int x, int y);
    void RightButtonDown(int x, int y);
    void RightButtonUp(int x, int y);
    void MiddleButtonDown(int x, int y);
    void MiddleButtonUp(int x, int y);
}
