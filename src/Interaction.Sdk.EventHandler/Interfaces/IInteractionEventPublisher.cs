namespace JasonShave.Azure.Communication.Service.Interaction.Sdk.EventHandler.Interfaces;

public interface IInteractionEventPublisher
{
    void Publish(BinaryData binaryPayload, string eventName, string contextId = default!);
}