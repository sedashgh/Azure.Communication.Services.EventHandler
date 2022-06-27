using JasonShave.Azure.Communication.Service.CallingServer.EventHandler.Abstractions;
using JasonShave.Azure.Communication.Service.CallingServer.EventHandler.Version_2022_11_1.Dispatcher;
using Microsoft.Extensions.DependencyInjection;

namespace JasonShave.Azure.Communication.Service.CallingServer.EventHandler
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAzureCommunicationServicesEventHandler_V2022_11_1(
            this IServiceCollection services)
        {
            // non-version specific
            services.AddSingleton<IEventConverter, JsonEventConverter>();
            services.AddSingleton<IEventCatalog, EventCatalogService>();
            services.AddSingleton<IEventPublisher, CallingServerEventPublisher>();

            // version 2022-11-1 services
            services.AddSingleton<IEventDispatcher, CallingServerEventDispatcher>();
            services.AddSingleton<ICallingServerEventSubscriber, CallingServerEventDispatcher>();

            return services;
        }
    }
}