using FriendlyWin32.WinApi;

namespace FriendlyWin32.Apis;

internal interface KeyboardApi
{
    public static IDisposable Subscribe<TMessage>(Action<TMessage> handler) => WindowsHookEx.Subscribe(handler);
}
