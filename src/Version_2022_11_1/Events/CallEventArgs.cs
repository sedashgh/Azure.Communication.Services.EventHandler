namespace JasonShave.Azure.Communication.Service.CallingServer.Extensions.Version_2022_11_1.Events;

public class CallEventArgs<TEvent> : EventArgs
    where TEvent : class
{
    public TEvent Event { get; init; }
}