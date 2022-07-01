using Microsoft.Extensions.DependencyInjection;

namespace JasonShave.Azure.Communication.Service.EventHandler.Abstractions;

public class EventHandlerBuilder
{
    public IServiceCollection Services { get; }

    public EventHandlerBuilder(IServiceCollection services) => Services = services;
}