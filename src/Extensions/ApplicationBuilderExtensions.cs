using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Events;
using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace JasonShave.Azure.Communication.Service.CallingServer.Extensions.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseAzureCallingServerEventDispatcher<TVersion>(this IApplicationBuilder app)
    {
        var eventCatalog = app.ApplicationServices.GetRequiredService<IEventCatalog<TVersion>>();
        eventCatalog
            .Register<CallConnectedEvent>()
            .Register<CallDisconnectedEvent>()
            .Register<CallConnectionStateChanged>();
    }
}