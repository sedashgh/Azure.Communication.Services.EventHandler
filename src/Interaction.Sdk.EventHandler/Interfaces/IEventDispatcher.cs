namespace JasonShave.Azure.Communication.Service.Interaction.Sdk.EventHandler.Interfaces;

internal interface IEventDispatcher
{
    void Dispatch(object @event, Type eventType, string contextId = default!);
}