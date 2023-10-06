namespace Core.Model;
public class ScriptEventDTO
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Icon { get; set; }

    public string ExtraInfo { get; set; }

    public TimeSpan Delay { get; set; }

    public string EventType { get; set; }

    public int X { get; set; }

    public int Y { get; set; }

    public int Key { get; set; }
}
