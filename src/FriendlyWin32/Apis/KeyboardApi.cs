using Win32.Hooks;

namespace Win32.Apis;

internal interface KeyboardApi
{
    public static IDisposable Subscribe<TMessage>(Action<TMessage> handler) => WindowsHookEx.Subscribe(handler);
}
