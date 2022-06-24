using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Interfaces;
using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Version_2022_11_1.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace JasonShave.Azure.Communication.Service.CallingServer.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseAzureCallingServerEventDispatcher<TVersion>(this IApplicationBuilder app)
    {
        var eventCatalog = app.ApplicationServices.GetRequiredService<IEventCatalog<TVersion>>();
        eventCatalog
            .Register<CallConnectedEvent>()
            .Register<CallDisconnectedEvent>()
            .Register<CallConnectionStateChanged>();

        return app;
    }
}