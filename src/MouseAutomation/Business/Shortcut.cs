using FriendlyWin32.Models;
using System.Collections.Generic;

namespace MouseAutomation.Business;
internal class Shortcut
{
    public Shortcut(VirtualKey key, IList<VirtualKey> modifierKeys)
    {
        Key = key;
        ModifierKeys = modifierKeys;
    }

    public VirtualKey Key { get; }
    public IList<VirtualKey> ModifierKeys { get; }
}
