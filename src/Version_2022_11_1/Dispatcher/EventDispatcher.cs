using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Events;
using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Interfaces;
using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Models;

namespace JasonShave.Azure.Communication.Service.CallingServer.Extensions.Version_2022_11_1.Dispatcher;

public class EventDispatcher<TVersion> : IEventDispatcher<TVersion>
    where TVersion : EventVersion
{
    // version 2020-11-1 events
    public event EventHandler<CallEventArgs<CallConnectedEvent>>? OnCallConnected;
    public event EventHandler<CallEventArgs<CallDisconnectedEvent>>? OnCallDisconnected;
    public event EventHandler<CallEventArgs<CallConnectionStateChanged>>? OnCallConnectionStateChanged;

    private readonly Dictionary<Type, Action<object>> _eventDictionary = new();

    public EventDispatcher()
    {
        _eventDictionary = new Dictionary<Type, Action<object>>
        {
            [typeof(CallConnectedEvent)] = evt => OnCallConnected?.Invoke(this, new CallEventArgs<CallConnectedEvent> { Event = (CallConnectedEvent)evt }),
            [typeof(CallDisconnectedEvent)] = evt => OnCallDisconnected?.Invoke(this, new CallEventArgs<CallDisconnectedEvent> { Event = (CallDisconnectedEvent)evt }),
            [typeof(CallConnectionStateChanged)] = evt => OnCallConnectionStateChanged?.Invoke(this, new CallEventArgs<CallConnectionStateChanged> { Event = (CallConnectionStateChanged)evt }),
        };
    }

    public void Dispatch(object @event)
    {
        if (@event is null) return;
        var eventType = @event.GetType();
        _eventDictionary[eventType](@event);
    }
}