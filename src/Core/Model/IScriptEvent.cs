namespace Core.Model;
public interface IScriptEvent
{
    public Task Execute(CancellationToken cancellationToken);
}
