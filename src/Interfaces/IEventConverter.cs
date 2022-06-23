namespace JasonShave.Azure.Communication.Service.CallingServer.Extensions.Interfaces;

public interface IEventConverter
{
    object? Convert(string eventPayload, Type eventType);
}