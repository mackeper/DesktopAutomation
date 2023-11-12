namespace Core.Model;
public class Function : IFunction
{
    public Guid Id { get; }

    public string Name { get; set; }

    public string Description { get; set; }

    public IList<IScriptEvent> Events => new List<IScriptEvent>();

    public void Execute()
    {
        throw new NotImplementedException();
    }

    public Function(Guid id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
}