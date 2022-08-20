// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

namespace JasonShave.Azure.Communication.Service.EventHandler;

internal class EventCatalogService<TPrimitive> : IEventCatalog<TPrimitive>
    where TPrimitive : IPrimitive
{
    private readonly Dictionary<string, Type> _eventCatalogByName = new();
    private readonly Dictionary<Type, string> _eventCatalogByType = new();

    public IEventCatalog<TPrimitive> Register<TEvent>()
    {
        _eventCatalogByName.Add(typeof(TEvent).Name, typeof(TEvent));
        _eventCatalogByType.Add(typeof(TEvent), $"{IPrimitive.EventPrefix}{typeof(TEvent)}");
        return this;
    }

    public Type Get(string eventName)
    {
        _eventCatalogByName.TryGetValue(eventName.Replace(IPrimitive.EventPrefix, ""), out var eventType);
        if (eventType is null)
            throw new ApplicationException($"Unable to determine the event {eventName} from the event catalog.");
        return eventType;
    }

    public string Get(Type eventType)
    {
        _eventCatalogByType.TryGetValue(eventType, out var eventName);
        if (eventName is null)
            throw new ApplicationException($"Unable to determine the event {eventType.Name} from the event catalog");
        return eventName;
    }

    public IEnumerable<Type> List() => _eventCatalogByName.Values.ToList();
}