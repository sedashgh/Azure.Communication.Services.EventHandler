namespace JasonShave.Azure.Communication.Service.EventHandler.Abstractions.Interfaces;

public interface IEventCatalog<TPrimitive>
{
    IEventCatalog<TPrimitive> Register<TEvent>();

    Type? Get(string eventName);
}