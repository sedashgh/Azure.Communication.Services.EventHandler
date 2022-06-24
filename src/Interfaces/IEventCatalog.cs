namespace JasonShave.Azure.Communication.Service.CallingServer.Extensions.Interfaces;

internal interface IEventCatalog<TVersion>
{
    IEventCatalog<TVersion> Register<TEvent>();

    Type? Get(string eventName);
}