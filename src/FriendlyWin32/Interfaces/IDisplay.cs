namespace FriendlyWin32.Interfaces;

public interface IDisplay : IDisposable
{
    IEnumerable<string> GetOpenWindowsTitles();
}