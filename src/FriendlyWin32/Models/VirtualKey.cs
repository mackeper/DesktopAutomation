using System.Diagnostics.CodeAnalysis;

namespace FriendlyWin32.Models;
public readonly struct VirtualKey : IEquatable<VirtualKey>
{
    public ushort Value { get; }
    public string Name { get; }
    public string ShortName { get; }
    public string Description { get; }

    private VirtualKey(ushort value, string name, string shortName, string description)
    {
        Value = value;
        Name = name;
        ShortName = shortName;
        Description = description;
    }

    public override bool Equals([NotNullWhen(true)] object? other)
        => other is VirtualKey key && Equals(other);

    public bool Equals(VirtualKey other)
        => Value == other.Value;

    public override int GetHashCode()
        => HashCode.Combine(Value);

    public static bool operator ==(VirtualKey left, VirtualKey right)
        => left.Equals(right);

    public static bool operator !=(VirtualKey left, VirtualKey right)
        => !(left == right);

    public static VirtualKey LBUTTON => new(0x01, nameof(LBUTTON), "LMB", "Left mouse button");
    public static VirtualKey RBUTTON => new(0x02, nameof(RBUTTON), "RMB", "Right mouse button");
    public static VirtualKey CANCEL => new(0x03, nameof(CANCEL), "", "Control-break processing");
    public static VirtualKey MBUTTON => new(0x04, nameof(MBUTTON), "MMB", "Middle mouse button");
    public static VirtualKey XBUTTON1 => new(0x05, nameof(XBUTTON1), "X1", "X1 mouse button");
    public static VirtualKey XBUTTON2 => new(0x06, nameof(XBUTTON2), "X2", "X2 mouse button");
    public static VirtualKey BACK => new(0x08, nameof(BACK), "", "BACKSPACE key");
    public static VirtualKey TAB => new(0x09, nameof(TAB), "", "TAB key");
    public static VirtualKey CLEAR => new(0x0C, nameof(CLEAR), "", "CLEAR key");
    public static VirtualKey RETURN => new(0x0D, nameof(RETURN), "", "ENTER key");
    public static VirtualKey SHIFT => new(0x10, nameof(SHIFT), "", "SHIFT key");
    public static VirtualKey CONTROL => new(0x11, nameof(CONTROL), "CTRL", "CTRL key");
    public static VirtualKey MENU => new(0x12, nameof(MENU), "ALT", "ALT key");
    public static VirtualKey PAUSE => new(0x13, nameof(PAUSE), "", "PAUSE key");
    public static VirtualKey CAPITAL => new(0x14, nameof(CAPITAL), "", "CAPS LOCK key");
    public static VirtualKey KANA => new(0x15, nameof(KANA), "", "IME Kana mode");
    public static VirtualKey HANGUL => new(0x15, nameof(HANGUL), "", "IME Hangul mode");
    public static VirtualKey IME_ON => new(0x16, nameof(IME_ON), "", "IME On");
    public static VirtualKey JUNJA => new(0x17, nameof(JUNJA), "", "IME Junja mode");
    public static VirtualKey FINAL => new(0x18, nameof(FINAL), "", "IME final mode");
    public static VirtualKey HANJA => new(0x19, nameof(HANJA), "", "IME Hanja mode");
    public static VirtualKey KANJI => new(0x19, nameof(KANJI), "", "IME Kanji mode");
    public static VirtualKey IME_OFF => new(0x1A, nameof(IME_OFF), "", "IME Off");
    public static VirtualKey ESCAPE => new(0x1B, nameof(ESCAPE), "", "ESC key");
    public static VirtualKey CONVERT => new(0x1C, nameof(CONVERT), "", "IME convert");
    public static VirtualKey NONCONVERT => new(0x1D, nameof(NONCONVERT), "", "IME nonconvert");
    public static VirtualKey ACCEPT => new(0x1E, nameof(ACCEPT), "", "IME accept");
    public static VirtualKey MODECHANGE => new(0x1F, nameof(MODECHANGE), "", "IME mode change request");
    public static VirtualKey SPACE => new(0x20, nameof(SPACE), "", "SPACEBAR");
    public static VirtualKey PRIOR => new(0x21, nameof(PRIOR), "", "PAGE UP key");
    public static VirtualKey NEXT => new(0x22, nameof(NEXT), "", "PAGE DOWN key");
    public static VirtualKey END => new(0x23, nameof(END), "", "END key");
    public static VirtualKey HOME => new(0x24, nameof(HOME), "", "HOME key");
    public static VirtualKey LEFT => new(0x25, nameof(LEFT), "", "LEFT ARROW key");
    public static VirtualKey UP => new(0x26, nameof(UP), "", "UP ARROW key");
    public static VirtualKey RIGHT => new(0x27, nameof(RIGHT), "", "RIGHT ARROW key");
    public static VirtualKey DOWN => new(0x28, nameof(DOWN), "", "DOWN ARROW key");
    public static VirtualKey SELECT => new(0x29, nameof(SELECT), "", "SELECT key");
    public static VirtualKey PRINT => new(0x2A, nameof(PRINT), "", "PRINT key");
    public static VirtualKey EXECUTE => new(0x2B, nameof(EXECUTE), "", "EXECUTE key");
    public static VirtualKey SNAPSHOT => new(0x2C, nameof(SNAPSHOT), "", "PRINT SCREEN key");
    public static VirtualKey INSERT => new(0x2D, nameof(INSERT), "", "INS key");
    public static VirtualKey DELETE => new(0x2E, nameof(DELETE), "", "DEL key");
    public static VirtualKey HELP => new(0x2F, nameof(HELP), "", "HELP key");
    public static VirtualKey _0 => new(0x30, "_0", "0", "0 key");
    public static VirtualKey _1 => new(0x31, "_1", "1", "1 key");
    public static VirtualKey _2 => new(0x32, "_2", "2", "2 key");
    public static VirtualKey _3 => new(0x33, "_3", "3", "3 key");
    public static VirtualKey _4 => new(0x34, "_4", "4", "4 key");
    public static VirtualKey _5 => new(0x35, "_5", "5", "5 key");
    public static VirtualKey _6 => new(0x36, "_6", "6", "6 key");
    public static VirtualKey _7 => new(0x37, "_7", "7", "7 key");
    public static VirtualKey _8 => new(0x38, "_8", "8", "8 key");
    public static VirtualKey _9 => new(0x39, "_9", "9", "9 key");
    public static VirtualKey A => new(0x41, nameof(A), "A", "A key");
    public static VirtualKey B => new(0x42, nameof(B), "B", "B key");
    public static VirtualKey C => new(0x43, nameof(C), "C", "C key");
    public static VirtualKey D => new(0x44, nameof(D), "D", "D key");
    public static VirtualKey E => new(0x45, nameof(E), "E", "E key");
    public static VirtualKey F => new(0x46, nameof(F), "F", "F key");
    public static VirtualKey G => new(0x47, nameof(G), "G", "G key");
    public static VirtualKey H => new(0x48, nameof(H), "H", "H key");
    public static VirtualKey I => new(0x49, nameof(I), "I", "I key");
    public static VirtualKey J => new(0x4A, nameof(J), "J", "J key");
    public static VirtualKey K => new(0x4B, nameof(K), "K", "K key");
    public static VirtualKey L => new(0x4C, nameof(L), "L", "L key");
    public static VirtualKey M => new(0x4D, nameof(M), "M", "M key");
    public static VirtualKey N => new(0x4E, nameof(N), "N", "N key");
    public static VirtualKey O => new(0x4F, nameof(O), "O", "O key");
    public static VirtualKey P => new(0x50, nameof(P), "P", "P key");
    public static VirtualKey Q => new(0x51, nameof(Q), "Q", "Q key");
    public static VirtualKey R => new(0x52, nameof(R), "R", "R key");
    public static VirtualKey S => new(0x53, nameof(S), "S", "S key");
    public static VirtualKey T => new(0x54, nameof(T), "T", "T key");
    public static VirtualKey U => new(0x55, nameof(U), "U", "U key");
    public static VirtualKey V => new(0x56, nameof(V), "V", "V key");
    public static VirtualKey W => new(0x57, nameof(W), "W", "W key");
    public static VirtualKey X => new(0x58, nameof(X), "X", "X key");
    public static VirtualKey Y => new(0x59, nameof(Y), "Y", "Y key");
    public static VirtualKey Z => new(0x5A, nameof(Z), "Z", "Z key");
    public static VirtualKey LWIN => new(0x5B, nameof(LWIN), "LWin", "Left Windows key");
    public static VirtualKey RWIN => new(0x5C, nameof(RWIN), "RWin", "Right Windows key");
    public static VirtualKey APPS => new(0x5D, nameof(APPS), "APP", "Applications key");
    public static VirtualKey SLEEP => new(0x5F, nameof(SLEEP), "SLP", "Computer Sleep key");
    public static VirtualKey NUMPAD0 => new(0x60, nameof(NUMPAD0), "Numpad 0", "Numeric keypad 0 key");
    public static VirtualKey NUMPAD1 => new(0x61, nameof(NUMPAD1), "Numpad 1", "Numeric keypad 1 key");
    public static VirtualKey NUMPAD2 => new(0x62, nameof(NUMPAD2), "Numpad 2", "Numeric keypad 2 key");
    public static VirtualKey NUMPAD3 => new(0x63, nameof(NUMPAD3), "Numpad 3", "Numeric keypad 3 key");
    public static VirtualKey NUMPAD4 => new(0x64, nameof(NUMPAD4), "Numpad 4", "Numeric keypad 4 key");
    public static VirtualKey NUMPAD5 => new(0x65, nameof(NUMPAD5), "Numpad 5", "Numeric keypad 5 key");
    public static VirtualKey NUMPAD6 => new(0x66, nameof(NUMPAD6), "Numpad 6", "Numeric keypad 6 key");
    public static VirtualKey NUMPAD7 => new(0x67, nameof(NUMPAD7), "Numpad 7", "Numeric keypad 7 key");
    public static VirtualKey NUMPAD8 => new(0x68, nameof(NUMPAD8), "Numpad 8", "Numeric keypad 8 key");
    public static VirtualKey NUMPAD9 => new(0x69, nameof(NUMPAD9), "Numpad 9", "Numeric keypad 9 key");
    public static VirtualKey MULTIPLY => new(0x6A, nameof(MULTIPLY), "*", "Multiply key");
    public static VirtualKey ADD => new(0x6B, nameof(ADD), "+", "Add key");
    public static VirtualKey SEPARATOR => new(0x6C, nameof(SEPARATOR), "", "Separator key");
    public static VirtualKey SUBTRACT => new(0x6D, nameof(SUBTRACT), "-", "Subtract key");
    public static VirtualKey DECIMAL => new(0x6E, nameof(DECIMAL), ".", "Decimal key");
    public static VirtualKey DIVIDE => new(0x6F, nameof(DIVIDE), "/", "Divide key");
    public static VirtualKey F1 => new(0x70, nameof(F1), "F1", "F1 key");
    public static VirtualKey F2 => new(0x71, nameof(F2), "F2", "F2 key");
    public static VirtualKey F3 => new(0x72, nameof(F3), "F3", "F3 key");
    public static VirtualKey F4 => new(0x73, nameof(F4), "F4", "F4 key");
    public static VirtualKey F5 => new(0x74, nameof(F5), "F5", "F5 key");
    public static VirtualKey F6 => new(0x75, nameof(F6), "F6", "F6 key");
    public static VirtualKey F7 => new(0x76, nameof(F7), "F7", "F7 key");
    public static VirtualKey F8 => new(0x77, nameof(F8), "F8", "F8 key");
    public static VirtualKey F9 => new(0x78, nameof(F9), "F9", "F9 key");
    public static VirtualKey F10 => new(0x79, nameof(F10), "F10", "F10 key");
    public static VirtualKey F11 => new(0x7A, nameof(F11), "F11", "F11 key");
    public static VirtualKey F12 => new(0x7B, nameof(F12), "F12", "F12 key");
    public static VirtualKey F13 => new(0x7C, nameof(F13), "F13", "F13 key");
    public static VirtualKey F14 => new(0x7D, nameof(F14), "F14", "F14 key");
    public static VirtualKey F15 => new(0x7E, nameof(F15), "F15", "F15 key");
    public static VirtualKey F16 => new(0x7F, nameof(F16), "F16", "F16 key");
    public static VirtualKey F17 => new(0x80, nameof(F17), "F17", "F17 key");
    public static VirtualKey F18 => new(0x81, nameof(F18), "F18", "F18 key");
    public static VirtualKey F19 => new(0x82, nameof(F19), "F19", "F19 key");
    public static VirtualKey F20 => new(0x83, nameof(F20), "F20", "F20 key");
    public static VirtualKey F21 => new(0x84, nameof(F21), "F21", "F21 key");
    public static VirtualKey F22 => new(0x85, nameof(F22), "F22", "F22 key");
    public static VirtualKey F23 => new(0x86, nameof(F23), "F23", "F23 key");
    public static VirtualKey F24 => new(0x87, nameof(F24), "F24", "F24 key");
    public static VirtualKey NUMLOCK => new(0x90, nameof(NUMLOCK), "NUM", "NUM LOCK key");
    public static VirtualKey SCROLL => new(0x91, nameof(SCROLL), "SCL", "SCROLL LOCK key");
    public static VirtualKey LSHIFT => new(0xA0, nameof(LSHIFT), "LShift", "Left SHIFT key");
    public static VirtualKey RSHIFT => new(0xA1, nameof(RSHIFT), "RShift", "Right SHIFT key");
    public static VirtualKey LCONTROL => new(0xA2, nameof(LCONTROL), "LCtrl", "Left CONTROL key");
    public static VirtualKey RCONTROL => new(0xA3, nameof(RCONTROL), "RCtrl", "Right CONTROL key");
    public static VirtualKey LMENU => new(0xA4, nameof(LMENU), "LAlt", "Left ALT key");
    public static VirtualKey RMENU => new(0xA5, nameof(RMENU), "RAlt", "Right ALT key");
    public static VirtualKey BROWSER_BACK => new(0xA6, nameof(BROWSER_BACK), "Back", "Browser Back key");
    public static VirtualKey BROWSER_FORWARD => new(0xA7, nameof(BROWSER_FORWARD), "Forw", "Browser Forward key");
    public static VirtualKey BROWSER_REFRESH => new(0xA8, nameof(BROWSER_REFRESH), "Refr", "Browser Refresh key");
    public static VirtualKey BROWSER_STOP => new(0xA9, nameof(BROWSER_STOP), "Stop", "Browser Stop key");
    public static VirtualKey BROWSER_SEARCH => new(0xAA, nameof(BROWSER_SEARCH), "Search", "Browser Search key");
    public static VirtualKey BROWSER_FAVORITES => new(0xAB, nameof(BROWSER_FAVORITES), "Fav", "Browser Favorites key");
    public static VirtualKey BROWSER_HOME => new(0xAC, nameof(BROWSER_HOME), "S/Home", "Browser Start and Home key");
    public static VirtualKey VOLUME_MUTE => new(0xAD, nameof(VOLUME_MUTE), "VMute", "Volume Mute key");
    public static VirtualKey VOLUME_DOWN => new(0xAE, nameof(VOLUME_DOWN), "VDown", "Volume Down key");
    public static VirtualKey VOLUME_UP => new(0xAF, nameof(VOLUME_UP), "VUp", "Volume Up key");
    public static VirtualKey MEDIA_NEXT_TRACK => new(0xB0, nameof(MEDIA_NEXT_TRACK), "Next", "Next Track key");
    public static VirtualKey MEDIA_PREV_TRACK => new(0xB1, nameof(MEDIA_PREV_TRACK), "Prev", "Previous Track key");
    public static VirtualKey MEDIA_STOP => new(0xB2, nameof(MEDIA_STOP), "Stop", "Stop Media key");
    public static VirtualKey MEDIA_PLAY_PAUSE => new(0xB3, nameof(MEDIA_PLAY_PAUSE), "Play", "Play/Pause Media key");
    public static VirtualKey LAUNCH_MAIL => new(0xB4, nameof(LAUNCH_MAIL), "Mail", "Start Mail key");
    public static VirtualKey LAUNCH_MEDIA_SELECT => new(0xB5, nameof(LAUNCH_MEDIA_SELECT), "Meda", "Select Media key");
    public static VirtualKey LAUNCH_APP1 => new(0xB6, nameof(LAUNCH_APP1), "App1", "Start Application 1 key");
    public static VirtualKey LAUNCH_APP2 => new(0xB7, nameof(LAUNCH_APP2), "App2", "Start Application 2 key");
    public static VirtualKey OEM_1 => new(0xBA, nameof(OEM_1), "OEM_1", "Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the ;: key");
    public static VirtualKey OEM_PLUS => new(0xBB, nameof(OEM_PLUS), "+", "For any country/region, the + key");
    public static VirtualKey OEM_COMMA => new(0xBC, nameof(OEM_COMMA), ",", "For any country/region, the , key");
    public static VirtualKey OEM_MINUS => new(0xBD, nameof(OEM_MINUS), "-", "For any country/region, the - key");
    public static VirtualKey OEM_PERIOD => new(0xBE, nameof(OEM_PERIOD), ".", "For any country/region, the . key");
    public static VirtualKey OEM_2 => new(0xBF, nameof(OEM_2), "", "Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the /? key");
    public static VirtualKey OEM_3 => new(0xC0, nameof(OEM_3), "", "Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the `~ key");
    public static VirtualKey OEM_4 => new(0xDB, nameof(OEM_4), "", "Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the [{ key");
    public static VirtualKey OEM_5 => new(0xDC, nameof(OEM_5), "", "Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the \\| key");
    public static VirtualKey OEM_6 => new(0xDD, nameof(OEM_6), "", "Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the ]} key");
    public static VirtualKey OEM_7 => new(0xDE, nameof(OEM_7), "", "Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the ' \" ' key");
    public static VirtualKey OEM_8 => new(0xDF, nameof(OEM_8), "", "Used for miscellaneous characters; it can vary by keyboard.");
    public static VirtualKey OEM_102 => new(0xE2, nameof(OEM_102), "", "Either the angle bracket key or the backslash key on the RT 102-key keyboard");
    public static VirtualKey PROCESSKEY => new(0xE5, nameof(PROCESSKEY), "", "IME PROCESS key");
    public static VirtualKey PACKET => new(0xE7, nameof(PACKET), "", "Used to pass Unicode characters as if they were keystrokes. The PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods");
    public static VirtualKey ATTN => new(0xF6, nameof(ATTN), "", "Attn key");
    public static VirtualKey CRSEL => new(0xF7, nameof(CRSEL), "", "CrSel key");
    public static VirtualKey EXSEL => new(0xF8, nameof(EXSEL), "", "ExSel key");
    public static VirtualKey EREOF => new(0xF9, nameof(EREOF), "", "Erase EOF key");
    public static VirtualKey PLAY => new(0xFA, nameof(PLAY), "", "Play key");
    public static VirtualKey ZOOM => new(0xFB, nameof(ZOOM), "", "Zoom key");
    public static VirtualKey NONAME => new(0xFC, nameof(NONAME), "", "Reserved");
    public static VirtualKey PA1 => new(0xFD, nameof(PA1), "", "PA1 key");
    public static VirtualKey OEM_CLEAR => new(0xFE, nameof(OEM_CLEAR), "", "Clear key");

    public static VirtualKey FromKeyCode(int keyCode)
    => keyCode switch
    {
        0x01 => LBUTTON,
        0x02 => RBUTTON,
        0x03 => CANCEL,
        0x04 => MBUTTON,
        0x05 => XBUTTON1,
        0x06 => XBUTTON2,
        0x08 => BACK,
        0x09 => TAB,
        0x0C => CLEAR,
        0x0D => RETURN,
        0x10 => SHIFT,
        0x11 => CONTROL,
        0x12 => MENU,
        0x13 => PAUSE,
        0x14 => CAPITAL,
        0x15 => KANA,
        // 0x15 => HANGUL,
        0x16 => IME_ON,
        0x17 => JUNJA,
        0x18 => FINAL,
        0x19 => HANJA,
        // 0x19 => KANJI,
        0x1A => IME_OFF,
        0x1B => ESCAPE,
        0x1C => CONVERT,
        0x1D => NONCONVERT,
        0x1E => ACCEPT,
        0x1F => MODECHANGE,
        0x20 => SPACE,
        0x21 => PRIOR,
        0x22 => NEXT,
        0x23 => END,
        0x24 => HOME,
        0x25 => LEFT,
        0x26 => UP,
        0x27 => RIGHT,
        0x28 => DOWN,
        0x29 => SELECT,
        0x2A => PRINT,
        0x2B => EXECUTE,
        0x2C => SNAPSHOT,
        0x2D => INSERT,
        0x2E => DELETE,
        0x2F => HELP,
        0x30 => _0,
        0x31 => _1,
        0x32 => _2,
        0x33 => _3,
        0x34 => _4,
        0x35 => _5,
        0x36 => _6,
        0x37 => _7,
        0x38 => _8,
        0x39 => _9,
        0x41 => A,
        0x42 => B,
        0x43 => C,
        0x44 => D,
        0x45 => E,
        0x46 => F,
        0x47 => G,
        0x48 => H,
        0x49 => I,
        0x4A => J,
        0x4B => K,
        0x4C => L,
        0x4D => M,
        0x4E => N,
        0x4F => O,
        0x50 => P,
        0x51 => Q,
        0x52 => R,
        0x53 => S,
        0x54 => T,
        0x55 => U,
        0x56 => V,
        0x57 => W,
        0x58 => X,
        0x59 => Y,
        0x5A => Z,
        0x5B => LWIN,
        0x5C => RWIN,
        0x5D => APPS,
        0x5F => SLEEP,
        0x60 => NUMPAD0,
        0x61 => NUMPAD1,
        0x62 => NUMPAD2,
        0x63 => NUMPAD3,
        0x64 => NUMPAD4,
        0x65 => NUMPAD5,
        0x66 => NUMPAD6,
        0x67 => NUMPAD7,
        0x68 => NUMPAD8,
        0x69 => NUMPAD9,
        0x6A => MULTIPLY,
        0x6B => ADD,
        0x6C => SEPARATOR,
        0x6D => SUBTRACT,
        0x6E => DECIMAL,
        0x6F => DIVIDE,
        0x70 => F1,
        0x71 => F2,
        0x72 => F3,
        0x73 => F4,
        0x74 => F5,
        0x75 => F6,
        0x76 => F7,
        0x77 => F8,
        0x78 => F9,
        0x79 => F10,
        0x7A => F11,
        0x7B => F12,
        0x7C => F13,
        0x7D => F14,
        0x7E => F15,
        0x7F => F16,
        0x80 => F17,
        0x81 => F18,
        0x82 => F19,
        0x83 => F20,
        0x84 => F21,
        0x85 => F22,
        0x86 => F23,
        0x87 => F24,
        0x90 => NUMLOCK,
        0x91 => SCROLL,
        0xA0 => LSHIFT,
        0xA1 => RSHIFT,
        0xA2 => LCONTROL,
        0xA3 => RCONTROL,
        0xA4 => LMENU,
        0xA5 => RMENU,
        0xA6 => BROWSER_BACK,
        0xA7 => BROWSER_FORWARD,
        0xA8 => BROWSER_REFRESH,
        0xA9 => BROWSER_STOP,
        0xAA => BROWSER_SEARCH,
        0xAB => BROWSER_FAVORITES,
        0xAC => BROWSER_HOME,
        0xAD => VOLUME_MUTE,
        0xAE => VOLUME_DOWN,
        0xAF => VOLUME_UP,
        0xB0 => MEDIA_NEXT_TRACK,
        0xB1 => MEDIA_PREV_TRACK,
        0xB2 => MEDIA_STOP,
        0xB3 => MEDIA_PLAY_PAUSE,
        0xB4 => LAUNCH_MAIL,
        0xB5 => LAUNCH_MEDIA_SELECT,
        0xB6 => LAUNCH_APP1,
        0xB7 => LAUNCH_APP2,
        0xBA => OEM_1,
        0xBB => OEM_PLUS,
        0xBC => OEM_COMMA,
        0xBD => OEM_MINUS,
        0xBE => OEM_PERIOD,
        0xBF => OEM_2,
        0xC0 => OEM_3,
        0xDB => OEM_4,
        0xDC => OEM_5,
        0xDD => OEM_6,
        0xDE => OEM_7,
        0xDF => OEM_8,
        0xE1 => OEM_102,
        0xE2 => PROCESSKEY,
        0xE3 => PACKET,
        0xE5 => ATTN,
        0xE6 => CRSEL,
        0xE7 => EXSEL,
        0xE8 => EREOF,
        0xE9 => PLAY,
        0xEA => ZOOM,
        0xEB => NONAME,
        0xEC => PA1,
        0xED => OEM_CLEAR,
        _ => throw new ArgumentException("Invalid key code"),
    };

    /// <summary>
    /// Get all available virtual keys
    /// </summary>
    public static IEnumerable<VirtualKey> VirtualKeys
    {
        get
        {
            var list = new List<VirtualKey>();
            for (var i = 0; i < 0xFF; i++)
            {
                try
                {
                    list.Add(FromKeyCode(i));
                }
                catch (ArgumentException)
                {
                    // Ignore
                }
            }
            return list;
        }
    }
}
