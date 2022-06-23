using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Interfaces;
using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Models;

namespace JasonShave.Azure.Communication.Service.CallingServer.Extensions;

public class EventCatalog<TVersion> : IEventCatalog<TVersion> 
    where TVersion : EventVersion
{
    private readonly Dictionary<string, Type> _eventCatalog = new();

    public IEventCatalog<TVersion> Register<TEvent>()
    {
        _eventCatalog.Add(typeof(TEvent).Name, typeof(TEvent));
        return this;
    }

    public Type? Get(string eventName)
    {
        _eventCatalog.TryGetValue(eventName, out var eventType);
        return eventType;
    }
}