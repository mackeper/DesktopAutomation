using FriendlyWin32.Models.Enums;
using System;

namespace MouseAutomation.Controls;
internal record struct RecordStep(int Id, MouseEventType Type, int X, int Y, TimeSpan Delay);
