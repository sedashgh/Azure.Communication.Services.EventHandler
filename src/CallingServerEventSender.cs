using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Interfaces;
using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Models;

namespace JasonShave.Azure.Communication.Service.CallingServer.Extensions;

internal class CallingServerEventSender<TVersion> : ICallingServerEventSender
    where TVersion : EventVersion
{
    private readonly IEventCatalog<TVersion> _eventCatalog;
    private readonly IEventDispatcher<TVersion> _eventDispatcher;
    private readonly IEventConverter _eventConverter;

    public CallingServerEventSender(
        IEventCatalog<TVersion> eventCatalog,
        IEventDispatcher<TVersion> eventDispatcher,
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