namespace JasonShave.Azure.Communication.Service.Interaction.Sdk.Contracts.V2022_11_1.Events;

public class CallEventArgs<TEvent> : EventArgs
    where TEvent : class
{
    public TEvent Event { get; init; }
}