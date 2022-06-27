namespace JasonShave.Azure.Communication.Service.CallingServer.EventHandler.Abstractions;

public interface IEventConverter
{
    object? Convert(string stringPayload, Type eventType);

    object? Convert(BinaryData binaryPayload, Type eventType);
}