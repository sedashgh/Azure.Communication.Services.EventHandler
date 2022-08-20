// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using Azure.Messaging;
using Azure.Messaging.EventGrid;
using Microsoft.Extensions.Logging;

namespace JasonShave.Azure.Communication.Service.EventHandler;

/// <inheritdoc />
public class EventPublisher<TPrimitive> : IEventPublisher<TPrimitive>
    where TPrimitive : IPrimitive
{
    private readonly ILogger<EventPublisher<TPrimitive>> _logger;
    private readonly IEventDispatcher<TPrimitive> _eventDispatcher;
    private readonly IEventConverter<TPrimitive> _eventConverter;

    public EventPublisher(
        ILogger<EventPublisher<TPrimitive>> logger,
        IEventDispatcher<TPrimitive> eventDispatcher,
        IEventConverter<TPrimitive> eventConverter)
    {
        _logger = logger;
        _eventDispatcher = eventDispatcher;
        _eventConverter = eventConverter;
    }

    public void Publish(string data, string eventName, string? contextId)
    {
        Handle(() => _eventConverter.Convert(data, eventName), contextId);
    }

    public void Publish(CloudEvent cloudEvent, string? contextId)
    {
        Handle(() => _eventConverter.Convert(cloudEvent), contextId);
    }

    public void Publish(EventGridEvent eventGridEvent, string? contextId)
    {
        Handle(() => _eventConverter.Convert(eventGridEvent.Data.ToString(), eventGridEvent.EventType), contextId);
    }

    private void Handle(Func<object?> converterFunc, string? contextId)
    {
        try
        {
            var convertedEvent = converterFunc();
            _logger.LogInformation($"Event publisher handling: {convertedEvent.GetType().Name} | ContextId: {contextId}");

            _eventDispatcher.Dispatch(convertedEvent, contextId);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
        }
    }
}