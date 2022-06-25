using Azure.Communication.CallingServer;
using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Interfaces;
using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Version_2022_11_1.Dispatcher;
using Microsoft.Extensions.DependencyInjection;

namespace JasonShave.Azure.Communication.Service.CallingServer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAzureCommunicationServicesCallingServerClient(this IServiceCollection services, string connectionString)
        {
            AddServices(connectionString, services);

            return services;
        }

        public static IServiceCollection AddAzureCommunicationServicesCallingServerClient(this IServiceCollection services, Action<CallingServerClientSettings> configurationDelegate)
        {
            var callingServerConfiguration = new CallingServerClientSettings();
            configurationDelegate(callingServerConfiguration);

            AddServices(callingServerConfiguration.ConnectionString, services);

            return services;
        }

        private static void AddServices(string connectionString, IServiceCollection services)
        {
            services.AddSingleton(new CallingServerClient(new Uri("https://x-pma-euno-01.plat.skype.com:6448"), connectionString));
            services.AddSingleton<IEventConverter, JsonEventConverter>();

            // version 2022-11-1 services
            services.AddSingleton<IEventCatalog, EventCatalog>();
            services.AddSingleton<IEventDispatcher, CallingServerEventDispatcher>();
            services.AddSingleton<ICallingServerEventSender, CallingServerEventSender>();
            services.AddSingleton<ICallingServerEventSubscriber, CallingServerEventDispatcher>();
        }
    }
}