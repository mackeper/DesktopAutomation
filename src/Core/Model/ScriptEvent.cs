namespace Core.Model;
public abstract class ScriptEvent : IScriptEvent
{
    public int Id { get; }

    public abstract string Name { get; }

    public abstract string Icon { get; }

    public abstract string ExtraInfo { get; }

    public TimeSpan Delay { get; set; }

    public string EventType => GetType().Name;

    public ScriptEvent(int id, TimeSpan delay)
    {
        Id = id;
        Delay = delay;
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
        await Task.Delay(Delay, cancellationToken);
        if (cancellationToken.IsCancellationRequested)
            return;
        ExecuteEvent();
    }

    protected abstract void ExecuteEvent();

    public abstract void SetX(int x);

    public abstract void SetY(int y);

    public abstract void SetKey(int key);
}
