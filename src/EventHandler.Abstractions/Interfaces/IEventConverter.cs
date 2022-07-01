namespace JasonShave.Azure.Communication.Service.EventHandler.Abstractions.Interfaces;

public interface IEventConverter
{
    object? Convert(string stringPayload, Type eventType);

    object? Convert(BinaryData data, Type eventType);
}