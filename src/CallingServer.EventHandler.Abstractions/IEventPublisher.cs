namespace JasonShave.Azure.Communication.Service.CallingServer.EventHandler.Abstractions;

public interface IEventPublisher
{
    void Send(string stringPayload, string eventName);

    void Send(BinaryData binaryPayload, string eventName);
}