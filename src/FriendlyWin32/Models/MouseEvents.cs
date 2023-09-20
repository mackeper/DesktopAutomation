namespace Win32.Models.MouseEvents;

public readonly record struct MouseMoveEvent(int X, int Y);
public readonly record struct LeftButtonDownEvent(int X, int Y);
public readonly record struct LeftButtonUpEvent(int X, int Y);
public readonly record struct LeftButtonDoubleClickEvent(int X, int Y);
public readonly record struct RightButtonDownEvent(int X, int Y);
public readonly record struct RightButtonUpEvent(int X, int Y);
public readonly record struct RightButtonDoubleClickEvent(int X, int Y);
public readonly record struct MiddleButtonDownEvent(int X, int Y);
public readonly record struct MiddleButtonUpEvent(int X, int Y);
public readonly record struct MiddleButtonDoubleClickEvent(int X, int Y);
public readonly record struct MouseWheelEvent(int Delta);
public readonly record struct XButtonDownEvent(int XButton);
public readonly record struct XButtonUpEvent(int XButton);
public readonly record struct XButtonDoubleClickEvent(int XButton);
public readonly record struct MouseHorizontalWheelEvent(int Delta);
