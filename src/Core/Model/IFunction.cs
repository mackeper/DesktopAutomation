namespace Core.Model;
public interface IFunction
{
    Guid Id { get; }

    string Name { get; }

    string Description { get; }

    IList<IScriptEvent> Events { get; }

    void Execute();
}
