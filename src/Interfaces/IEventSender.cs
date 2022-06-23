namespace JasonShave.Azure.Communication.Service.CallingServer.Extensions.Interfaces;

public interface IEventSender
{
    void Send(string eventPayload, string eventName);
}