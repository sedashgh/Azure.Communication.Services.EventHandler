using JasonShave.Azure.Communication.Service.CallingServer.Contracts.V2022_11_1.Events;
using JasonShave.Azure.Communication.Service.CallingServer.EventHandler.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace JasonShave.Azure.Communication.Service.CallingServer.EventHandler;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseAzureCallingServerEventHandler_V2022_11_1(this IApplicationBuilder app)
    {
        var eventCatalog = app.ApplicationServices.GetRequiredService<IEventCatalog>();
        eventCatalog
            .Register<CallConnectedEvent>()
            .Register<CallDisconnectedEvent>()
            .Register<CallConnectionStateChanged>();

        return app;
    }
}