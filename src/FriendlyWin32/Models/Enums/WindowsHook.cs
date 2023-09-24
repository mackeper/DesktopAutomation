namespace FriendlyWin32.Models.Enums;

/// <summary>
/// Represents various Windows hooks, their descriptions, and scopes.
/// </summary>
[Flags]
internal enum WindowsHook : int
{
    /// <summary>
    /// Monitors messages before the system sends them to the destination window
    /// procedure. (Thread or global scope)
    /// </summary>
    WH_CALLWNDPROC = 4,

    /// <summary>
    /// Monitors messages after they have been processed by the destination window
    /// procedure. (Thread or global scope)
    /// </summary>
    WH_CALLWNDPROCRET = 12,

    /// <summary>
    /// Receives notifications useful to a CBT application. (Thread or global scope)
    /// </summary>
    WH_CBT = 5,

    /// <summary>
    /// Useful for debugging other hook procedures. (Thread or global scope)
    /// </summary>
    WH_DEBUG = 9,

    /// <summary>
    /// Called when the application's foreground thread is about to become idle.
    /// (Thread or global scope)
    /// </summary>
    WH_FOREGROUNDIDLE = 11,

    /// <summary>
    /// Monitors messages posted to a message queue. (Thread or global scope)
    /// </summary>
    WH_GETMESSAGE = 3,

    /// <summary>
    /// Posts messages previously recorded by a WH_JOURNALRECORD hook procedure.
    /// (Global only)
    /// </summary>
    WH_JOURNALPLAYBACK = 1,

    /// <summary>
    /// Records input messages posted to the system message queue. (Global only)
    /// </summary>
    WH_JOURNALRECORD = 0,

    /// <summary>
    /// Monitors keystroke messages. (Thread or global scope)
    /// </summary>
    WH_KEYBOARD = 2,

    /// <summary>
    /// Monitors low-level keyboard input events. (Global only)
    /// </summary>
    WH_KEYBOARD_LL = 13,

    /// <summary>
    /// Monitors mouse messages. (Thread or global scope)
    /// </summary>
    WH_MOUSE = 7,

    /// <summary>
    /// Monitors low-level mouse input events. (Global only)
    /// </summary>
    WH_MOUSE_LL = 14,

    /// <summary>
    /// Monitors messages generated as a result of an input event in a dialog box,
    /// message box, menu, or scroll bar. (Thread or global scope)
    /// </summary>
    WH_MSGFILTER = -1,

    /// <summary>
    /// Receives notifications useful to shell applications. (Thread or global scope)
    /// </summary>
    WH_SHELL = 10,

    /// <summary>
    /// Monitors messages generated as a result of an input event in a dialog box,
    /// message box, menu, or scroll bar for all applications in the same desktop
    /// as the calling thread. (Global only)
    /// </summary>
    WH_SYSMSGFILTER = 6
}
