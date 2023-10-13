namespace Core.Model;
public class Script : IScript
{
    public Guid Id { get; }

    public string Name { get; }

    public string FilePath { get; }

    public int Version { get; }

    public IList<IScriptEvent> Events { get; }

    public Script(Guid id, string name, string filePath, int version, IList<IScriptEvent> events)
    {
        Id = id;
        Name = name;
        FilePath = filePath;
        Version = version;
        Events = events;
    }
}
