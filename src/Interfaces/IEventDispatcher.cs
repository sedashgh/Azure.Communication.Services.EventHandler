namespace JasonShave.Azure.Communication.Service.CallingServer.Extensions.Interfaces;

internal interface IEventDispatcher
{
    void Dispatch(object @event);
}