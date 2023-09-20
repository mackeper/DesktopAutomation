using System.Collections.Concurrent;

namespace Domain;
internal class EventObserver<TEvent>
{
    private ConcurrentDictionary<Guid, Action<TEvent>> observers = new();
    public Guid AddObserver(Action<TEvent> callback)
    {
        var observerId = Guid.NewGuid();
        while (!observers.TryAdd(observerId, callback))
            observerId = Guid.NewGuid();
        return observerId;
    }

    public bool RemoveObserver(Guid observerId) => observers.TryRemove(observerId, out _);
}
