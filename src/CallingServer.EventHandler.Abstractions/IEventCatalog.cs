namespace JasonShave.Azure.Communication.Service.CallingServer.EventHandler.Abstractions;

public interface IEventCatalog
{
    IEventCatalog Register<TEvent>();

    Type? Get(string eventName);
}