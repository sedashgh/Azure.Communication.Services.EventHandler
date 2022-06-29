using JasonShave.Azure.Communication.Service.Interaction.Sdk.EventHandler.Interfaces;
using Microsoft.Extensions.Logging;

namespace JasonShave.Azure.Communication.Service.Interaction.Sdk.EventHandler;

internal class InteractionEventPublisher : IInteractionEventPublisher
{
    private readonly ILogger<InteractionEventPublisher> _logger;
    private readonly IEventCatalog _eventCatalog;
    private readonly IEventDispatcher _eventDispatcher;
    private readonly IEventConverter _eventConverter;

    public InteractionEventPublisher(
        ILogger<InteractionEventPublisher> logger,
        IEventCatalog eventCatalog,
        IEventDispatcher eventDispatcher,
        IEventConverter eventConverter)
    {
        _logger = logger;
        _eventCatalog = eventCatalog;
        _eventDispatcher = eventDispatcher;
        _eventConverter = eventConverter;
    }

    public void Publish(BinaryData binaryPayload, string eventName, string contextId)
    {
        _logger.LogDebug($"Interaction event publisher handling: {eventName}");
        var eventType = _eventCatalog.Get(eventName);
        if (eventType is null) return;

        var convertedEvent = _eventConverter.Convert(binaryPayload, eventType);

        if (convertedEvent is null) return;
        _eventDispatcher.Dispatch(convertedEvent, eventType, contextId);
    }
}