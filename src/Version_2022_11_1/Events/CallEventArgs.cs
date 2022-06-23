using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Models;

namespace JasonShave.Azure.Communication.Service.CallingServer.Extensions.Events;

public class CallEventArgs<TEvent> : EventArgs
    where TEvent : class
{
    public TEvent Event { get; init; }
}