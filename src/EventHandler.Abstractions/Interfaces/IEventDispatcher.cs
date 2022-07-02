namespace JasonShave.Azure.Communication.Service.EventHandler.Abstractions.Interfaces;

public interface IEventDispatcher<TPrimitive>
    where TPrimitive : IPrimitive
{
    void Dispatch(object @event, Type eventType, string contextId = default!);
}