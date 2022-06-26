using Azure.Communication.CallingServer;
using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Interfaces;
using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Version_2022_11_1.Dispatcher;
using Microsoft.Extensions.DependencyInjection;

namespace JasonShave.Azure.Communication.Service.CallingServer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAzureCommunicationServicesCallingServerClient(
            this IServiceCollection services, string connectionString, string? pmaEndpoint = null)
        {
            AddServices(services, connectionString, pmaEndpoint);

            return services;
        }

        public static IServiceCollection AddAzureCommunicationServicesCallingServerClient(
            this IServiceCollection services, Action<CallingServerClientSettings> configurationDelegate,
            string? pmaEndpoint = null)
        {
            var callingServerConfiguration = new CallingServerClientSettings();
            configurationDelegate(callingServerConfiguration);

            AddServices(services, callingServerConfiguration.ConnectionString, pmaEndpoint);

            return services;
        }

        private static void AddServices(IServiceCollection services, string connectionString, string? pmaEndpoint)
        {
            if (pmaEndpoint is not null)
            {
                services.AddSingleton(new CallingServerClient(new Uri(pmaEndpoint), connectionString));
            }
            else
            {
                services.AddSingleton(new CallingServerClient(connectionString));

            }

            services.AddSingleton<IEventConverter, JsonEventConverter>();

            // version 2022-11-1 services
            services.AddSingleton<IEventCatalog, EventCatalogService>();
            services.AddSingleton<IEventDispatcher, CallingServerEventDispatcher>();
            services.AddSingleton<ICallingServerEventSender, CallingServerEventPublisher>();
            services.AddSingleton<ICallingServerEventSubscriber, CallingServerEventDispatcher>();
        }
    }
}