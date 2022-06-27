using JasonShave.Azure.Communication.Service.CallingServer.Contracts.V2022_11_1.Events;

namespace JasonShave.Azure.Communication.Service.CallingServer.EventHandler;

public interface ICallingServerEventSubscriber
{
    event Func<CallConnectedEvent, ValueTask>? OnCallConnected;
    event Func<CallDisconnectedEvent, ValueTask>? OnCallDisconnected;
    event Func<CallConnectionStateChanged, ValueTask>? OnCallConnectionStateChanged;
}