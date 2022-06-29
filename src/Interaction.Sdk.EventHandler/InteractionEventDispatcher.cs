using JasonShave.Azure.Communication.Service.Interaction.Sdk.Contracts.V2022_11_1.Events;
using JasonShave.Azure.Communication.Service.Interaction.Sdk.EventHandler.Interfaces;

namespace JasonShave.Azure.Communication.Service.Interaction.Sdk.EventHandler;

public class InteractionEventDispatcher : IEventDispatcher, IInteractionEventSubscriber
{
    // version 2020-11-1 events
    public event Func<IncomingCall, string, Task>? OnIncomingCall;
    public event Func<CallConnectedEvent, string, Task>? OnCallConnected;
    public event Func<CallDisconnectedEvent, string, Task>? OnCallDisconnected;
    public event Func<CallConnectionStateChanged, string, Task>? OnCallConnectionStateChanged;

    private readonly Dictionary<Type, Func<object, string, Task>> _eventDictionary = new();

    public InteractionEventDispatcher()
    {
        _eventDictionary = new Dictionary<Type, Func<object, string, Task>>
        {
            [typeof(IncomingCall)] = async (@event, contextId) => await OnIncomingCall?.Invoke((IncomingCall)@event, contextId),
            [typeof(CallConnectedEvent)] = (@event, contextId) => OnCallConnected?.Invoke((CallConnectedEvent)@event, contextId),
            [typeof(CallDisconnectedEvent)] = (@event, contextId) => OnCallDisconnected?.Invoke((CallDisconnectedEvent)@event, contextId),
            [typeof(CallConnectionStateChanged)] = (@event, contextId) => OnCallConnectionStateChanged?.Invoke((CallConnectionStateChanged)@event, contextId),
        };
    }

    public void Dispatch(object @event, Type eventType, string contextId = default!)
    {
        _eventDictionary[eventType](@event, contextId);
    }
}