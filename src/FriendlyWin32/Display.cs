using FriendlyWin32.Apis;
using FriendlyWin32.Interfaces;

namespace FriendlyWin32;
public class Display : IDisplay
{
    public void Dispose() => throw new NotImplementedException();
    public void Draw() => ScreenApi.Draw();
}
