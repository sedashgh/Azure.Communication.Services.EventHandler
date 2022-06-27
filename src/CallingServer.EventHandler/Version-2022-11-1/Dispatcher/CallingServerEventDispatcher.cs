using JasonShave.Azure.Communication.Service.CallingServer.Contracts.V2022_11_1.Events;
using JasonShave.Azure.Communication.Service.CallingServer.EventHandler.Abstractions;

namespace JasonShave.Azure.Communication.Service.CallingServer.EventHandler.Version_2022_11_1.Dispatcher;

public class CallingServerEventDispatcher : IEventDispatcher, ICallingServerEventSubscriber
{
    // version 2020-11-1 events
    public event Func<CallConnectedEvent, ValueTask>? OnCallConnected;
    public event Func<CallDisconnectedEvent, ValueTask>? OnCallDisconnected;
    public event Func<CallConnectionStateChanged, ValueTask>? OnCallConnectionStateChanged;

    private readonly Dictionary<Type, Action<object>> _eventDictionary = new();

    public CallingServerEventDispatcher()
    {
        _eventDictionary = new Dictionary<Type, Action<object>>
        {
            [typeof(CallConnectedEvent)] = evt => OnCallConnected?.Invoke((CallConnectedEvent)evt),
            [typeof(CallDisconnectedEvent)] = evt => OnCallDisconnected?.Invoke((CallDisconnectedEvent)evt),
            [typeof(CallConnectionStateChanged)] = evt => OnCallConnectionStateChanged?.Invoke((CallConnectionStateChanged)evt),
        };
    }

    public void Dispatch(object @event)
    {
        if (@event is null) return;
        var eventType = @event.GetType();
        _eventDictionary[eventType](@event);
    }
}