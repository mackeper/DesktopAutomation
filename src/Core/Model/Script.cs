namespace Core.Model;
public class Script : IScript
{
    public string Name { get; }

    public string FilePath { get; }

    public int Version { get; }

    public IList<IScriptEvent> Events { get; }

    public Script(string name, string filePath, int version, IList<IScriptEvent> events)
    {
        Name = name;
        FilePath = filePath;
        Version = version;
        Events = events;
    }
}
