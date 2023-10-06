namespace Core.Model;

public readonly record struct MousePosition(int X, int Y)
{
    public override string ToString() => $"({X}, {Y})";
}
