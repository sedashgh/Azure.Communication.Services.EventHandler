using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Models;

namespace JasonShave.Azure.Communication.Service.CallingServer.Extensions.Interfaces;

internal interface IEventDispatcher<TVersion>
    where TVersion : EventVersion
{
    void Dispatch(object @event);
}