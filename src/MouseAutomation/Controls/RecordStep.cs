using System;
using Win32.Models.Enums;

namespace MouseAutomation.Controls;
internal record struct RecordStep(MouseEventType Type, int X, int Y, TimeSpan Delay);
