using JasonShave.Azure.Communication.Service.CallingServer.EventHandler.Abstractions;

namespace JasonShave.Azure.Communication.Service.CallingServer.EventHandler;

public class CallingServerEventPublisher : IEventPublisher
{
    private readonly IEventCatalog _eventCatalog;
    private readonly IEventDispatcher _eventDispatcher;
    private readonly IEventConverter _eventConverter;

    public CallingServerEventPublisher(
        IEventCatalog eventCatalog,
        IEventDispatcher eventDispatcher,
        IEventConverter eventConverter)
    {
        _eventCatalog = eventCatalog;
        _eventDispatcher = eventDispatcher;
        _eventConverter = eventConverter;
    }

    public void Send(string stringPayload, string eventName)
    {
        var eventType = _eventCatalog.Get(eventName);
        if (eventType is null) return;

        var convertedEvent = _eventConverter.Convert(stringPayload, eventType);

        if (convertedEvent is null) return;
        _eventDispatcher.Dispatch(convertedEvent);
    }

    public void Send(BinaryData binaryPayload, string eventName)
    {
        var eventType = _eventCatalog.Get(eventName);
        if (eventType is null) return;

        var convertedEvent = _eventConverter.Convert(binaryPayload, eventType);

        if (convertedEvent is null) return;
        _eventDispatcher.Dispatch(convertedEvent);
    }
}