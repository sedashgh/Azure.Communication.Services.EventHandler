using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Version_2022_11_1.Events;

namespace JasonShave.Azure.Communication.Service.CallingServer.Extensions.Interfaces;

public interface ICallingServerEventSubscriber
{
    event Func<CallConnectedEvent, ValueTask>? OnCallConnected;
    event Func<CallDisconnectedEvent, ValueTask>? OnCallDisconnected;
    event Func<CallConnectionStateChanged, ValueTask>? OnCallConnectionStateChanged;
}