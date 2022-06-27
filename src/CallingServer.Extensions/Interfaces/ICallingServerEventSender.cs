namespace JasonShave.Azure.Communication.Service.CallingServer.Extensions.Interfaces;

public interface ICallingServerEventSender
{
    void Send(string stringPayload, string eventName);

    void Send(BinaryData binaryPayload, string eventName);
}