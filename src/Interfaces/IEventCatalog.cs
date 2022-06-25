namespace JasonShave.Azure.Communication.Service.CallingServer.Extensions.Interfaces;

internal interface IEventCatalog
{
    IEventCatalog Register<TEvent>();

    Type? Get(string eventName);
}