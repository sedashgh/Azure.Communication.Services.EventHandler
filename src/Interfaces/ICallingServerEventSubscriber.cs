using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Version_2022_11_1.Events;

namespace JasonShave.Azure.Communication.Service.CallingServer.Extensions.Interfaces;

public interface ICallingServerEventSubscriber
{
    event EventHandler<CallEventArgs<CallConnectedEvent>>? OnCallConnected;
    event EventHandler<CallEventArgs<CallDisconnectedEvent>>? OnCallDisconnected;
    event EventHandler<CallEventArgs<CallConnectionStateChanged>>? OnCallConnectionStateChanged;
}