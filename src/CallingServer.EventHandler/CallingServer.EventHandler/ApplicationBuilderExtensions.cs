using JasonShave.Azure.Communication.Service.CallingServer.EventHandler.Abstractions;
using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Version_2022_11_1.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace JasonShave.Azure.Communication.Service.CallingServer.EventHandler;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseAzureCallingServerEventDispatcher(this IApplicationBuilder app)
    {
        var eventCatalog = app.ApplicationServices.GetRequiredService<IEventCatalog>();
        eventCatalog
            .Register<CallConnectedEvent>()
            .Register<CallDisconnectedEvent>()
            .Register<CallConnectionStateChanged>();

        return app;
    }
}