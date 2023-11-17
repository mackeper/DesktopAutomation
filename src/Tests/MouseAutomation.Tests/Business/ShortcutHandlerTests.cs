using FriendlyWin32.Interfaces;
using FriendlyWin32.Models;
using FriendlyWin32.Models.KeyboardEvents;
using MouseAutomation.Business;
using NSubstitute;
using Serilog;

namespace MouseAutomation.Tests.Business;
public static class ShortcutHandlerTests
{
    [Fact]
    public static void HandleKeyDownEvent_WhenShortcutIsRegistered_CallsAction()
    {
        // Arrange
        var log = Substitute.For<ILogger>();
        var keyboard = Substitute.For<IKeyboard>();
        Action<KeyDownEvent>? keyDownEvent = null;
        keyboard
            .Subscribe(Arg.Any<Action<KeyDownEvent>>())
            .Returns(x =>
            {
                keyDownEvent = x.Arg<Action<KeyDownEvent>>();
                return Substitute.For<IDisposable>();
            });
        var handler = new ShortcutHandler(keyboard, log);

        var actionWasCalled = false;
        void action() => actionWasCalled = true;
        var shortcut = new Shortcut(VirtualKey.A, new List<VirtualKey>());
        handler.RegisterShortcut(shortcut, action);

        // Act
        keyDownEvent?.Invoke(new KeyDownEvent(VirtualKey.A.Value));

        // Assert
        Assert.True(actionWasCalled);
    }

    [Fact]
    public static void HandleKeyDownEvent_WhenShortcutIsNotRegistered_DoesNotCallAction()
    {
        // Arrange
        var log = Substitute.For<ILogger>();
        var keyboard = Substitute.For<IKeyboard>();
        Action<KeyDownEvent>? keyDownEvent = null;
        keyboard
            .Subscribe(Arg.Any<Action<KeyDownEvent>>())
            .Returns(x =>
            {
                keyDownEvent = x.Arg<Action<KeyDownEvent>>();
                return Substitute.For<IDisposable>();
            });
        var handler = new ShortcutHandler(keyboard, log);

        var actionWasCalled = false;
        void action() => actionWasCalled = true;
        var shortcut = new Shortcut(VirtualKey.A, new List<VirtualKey>());
        handler.RegisterShortcut(shortcut, action);

        // Act
        keyDownEvent?.Invoke(new KeyDownEvent(VirtualKey.B.Value));

        // Assert
        Assert.False(actionWasCalled);
    }

    [Fact]
    public static void HandleKeyDownEvent_WhenShortcutIsUnregistered_CallsAction()
    {
        // Arrange
        var log = Substitute.For<ILogger>();
        var keyboard = Substitute.For<IKeyboard>();
        Action<KeyDownEvent>? keyDownEvent = null;
        keyboard
            .Subscribe(Arg.Any<Action<KeyDownEvent>>())
            .Returns(x =>
            {
                keyDownEvent = x.Arg<Action<KeyDownEvent>>();
                return Substitute.For<IDisposable>();
            });
        var handler = new ShortcutHandler(keyboard, log);

        var actionWasCalled = false;
        void action() => actionWasCalled = true;
        var shortcut = new Shortcut(VirtualKey.A, new List<VirtualKey>());
        handler.RegisterShortcut(shortcut, action);

        // Act
        handler.UnregisterShortcut(shortcut);
        keyDownEvent?.Invoke(new KeyDownEvent(VirtualKey.A.Value));

        // Assert
        Assert.False(actionWasCalled);
    }
}
