// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Extensions.Logging;

namespace JasonShave.Azure.Communication.Service.EventHandler;

/// <inheritdoc />
public class EventPublisher<TPrimitive> : IEventPublisher<TPrimitive>
    where TPrimitive : IPrimitive
{
    private readonly ILogger<EventPublisher<TPrimitive>> _logger;
    private readonly IEventCatalog<TPrimitive> _eventCatalog;
    private readonly IEventDispatcher<TPrimitive> _eventDispatcher;
    private readonly IEventConverter _eventConverter;

    public EventPublisher(
        ILogger<EventPublisher<TPrimitive>> logger,
        IEventCatalog<TPrimitive> eventCatalog,
        IEventDispatcher<TPrimitive> eventDispatcher,
        IEventConverter eventConverter)
    {
        _logger = logger;
        _eventCatalog = eventCatalog;
        _eventDispatcher = eventDispatcher;
        _eventConverter = eventConverter;
    }

    public void Publish(string data, string eventName, string contextId)
    {
        _logger.LogInformation($"Event publisher handling: {eventName} | ContextId: {contextId}");
        var eventType = _eventCatalog.Get(eventName);
        if (eventType is null)
            throw new InvalidOperationException($"Unable to determine the event {eventName} from the event catalog. | ContextId: {contextId}");

        var convertedEvent = _eventConverter.Convert(data, eventType);

        if (convertedEvent is null)
            throw new InvalidOperationException($"Unable to convert type {eventName} | ContextId: {contextId}");

        _eventDispatcher.Dispatch(convertedEvent, eventType, contextId);
    }
}