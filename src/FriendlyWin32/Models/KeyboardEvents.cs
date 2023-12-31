﻿namespace FriendlyWin32.Models.KeyboardEvents;

public readonly record struct KeyDownEvent(int KeyCode);
public readonly record struct KeyUpEvent(int KeyCode);
public readonly record struct SystemKeyDownEvent(int KeyCode);
public readonly record struct SystemKeyUpEvent(int KeyCode);
