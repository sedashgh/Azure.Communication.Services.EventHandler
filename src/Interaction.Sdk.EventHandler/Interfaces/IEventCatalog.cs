namespace JasonShave.Azure.Communication.Service.Interaction.Sdk.EventHandler.Interfaces;

internal interface IEventCatalog
{
    IEventCatalog Register<TEvent>();

    Type? Get(string eventName);
}