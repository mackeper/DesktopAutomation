using FriendlyWin32.Models.Enums;

namespace Core.Model;
public record struct RecordStep(int Id, MouseEventType Type, int X, int Y, TimeSpan Delay);
