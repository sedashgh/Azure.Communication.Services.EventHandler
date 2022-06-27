namespace JasonShave.Azure.Communication.Service.CallingServer.EventHandler.Abstractions;

public interface IEventDispatcher
{
    void Dispatch(object @event);
}