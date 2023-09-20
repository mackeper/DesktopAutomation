using System.Collections.Concurrent;

namespace Win32.Infrastructure;
internal class MessageBus : IDisposable
{
    private sealed record Subscription(MessageBus MessageBus, Action<object> Handler, Type MessageType, Action? PostUnsubscribeAction = null) : IDisposable
    {
        public void Dispose()
        {
            if (MessageBus.subscribers.ContainsKey(MessageType))
                MessageBus.subscribers[MessageType].Remove(this);
            PostUnsubscribeAction?.Invoke();
        }
    }

    private readonly ConcurrentDictionary<Type, List<Subscription>> subscribers = new();

    public IDisposable Subscribe<TMessage>(Action<TMessage> handler)
    {
        var messageType = typeof(TMessage);

        if (!subscribers.ContainsKey(messageType))
            subscribers[messageType] = new List<Subscription>();

        var subscription = new Subscription(this, message => handler((TMessage)message), messageType);
        subscribers[messageType].Add(subscription);
        return subscription;
    }

    public bool Publish<TMessage>(TMessage message)
    {
        var messageType = typeof(TMessage);
        if (message is not null && subscribers.ContainsKey(messageType))
        {
            subscribers[messageType].ForEach(subscriber => subscriber.Handler(message));
            return true;
        }
        return false;
    }

    public void Dispose()
    {
        foreach (var (messageType, subscriptions) in subscribers)
            foreach (var subscription in subscriptions)
                subscription.Dispose();

        subscribers.Clear();
    }
}
