namespace JasonShave.Azure.Communication.Service.Interaction.Sdk.EventHandler.Interfaces;

internal interface IEventConverter
{
    object? Convert(string stringPayload, Type eventType);

    object? Convert(BinaryData binaryPayload, Type eventType);
}