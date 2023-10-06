using FriendlyWin32.Models;

namespace FriendlyWin32.Interfaces;
public interface IMouse : IDisposable
{
    public IDisposable Subscribe<TMessage>(Action<TMessage> handler);

    void Click();
    void Click(MousePosition mousePosition);
    void Click(int x, int y);

    void MoveRelative(int x, int y);
    void MoveAbsolute(int x, int y);

    void LeftButtonDown();
    void LeftButtonDown(int x, int y);
    void LeftButtonUp(int x, int y);

    void RightButtonDown(int x, int y);
    void RightButtonUp(int x, int y);

    void MiddleButtonDown(int x, int y);
    void MiddleButtonUp(int x, int y);

    MousePosition GetPosition();
}
