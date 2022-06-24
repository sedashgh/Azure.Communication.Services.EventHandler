namespace JasonShave.Azure.Communication.Service.CallingServer.Extensions.Interfaces;

internal interface IEventConverter
{
    object? Convert(string stringPayload, Type eventType);

    object? Convert(BinaryData binaryPayload, Type eventType);
}