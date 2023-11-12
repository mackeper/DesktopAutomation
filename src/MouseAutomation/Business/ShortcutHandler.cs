using FriendlyWin32.Interfaces;
using FriendlyWin32.Models;
using FriendlyWin32.Models.KeyboardEvents;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MouseAutomation.Business
{
    internal class ShortcutHandler
    {
        private readonly IKeyboard keyboard;
        private readonly List<(Shortcut, Action)> registeredShortcuts = new();

        public ShortcutHandler(IKeyboard keyboard)
        {
            this.keyboard = keyboard;
            keyboard.Subscribe<KeyDownEvent>(HandleKeyDownEvent);
        }

        public void RegisterShortcut(Shortcut shortcut, Action action)
            => registeredShortcuts.Add((shortcut, action));

        public void UnregisterShortcut(Shortcut shortcut)
            => registeredShortcuts.RemoveAll(x => x.Item1 == shortcut);

        private void HandleKeyDownEvent(KeyDownEvent @event)
        {
            foreach (var (shortcut, action) in registeredShortcuts)
            {
                try
                {
                    var key = VirtualKey.FromKeyCode(@event.KeyCode);
                    var modifiers = keyboard.GetCurrentModifiers().Select(VirtualKey.FromKeyCode).ToList();
                    if (ShortcutMatches(key, modifiers, shortcut))
                    {
                        action.Invoke();
                        break; // Break after the first matching shortcut is found
                    }
                }
                catch (ArgumentException e)
                {
                    // TODO: inject logger
                    Log.Warning(e, "Could not convert key code {0} to virtual key", @event.KeyCode);
                }
            }
        }

        private static bool ShortcutMatches(VirtualKey pressedKey, IEnumerable<VirtualKey> pressedModifiers, Shortcut shortcut)
            => pressedKey == shortcut.Key
            && new HashSet<VirtualKey>(pressedModifiers)
                .SetEquals(shortcut.ModifierKeys);
    }
}
