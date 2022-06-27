using Azure.Communication.CallingServer;
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
            this IServiceCollection services, Action<CallingServerClientSettings> clientSettingsDelegate,
            string? pmaEndpoint = null)
        {
            var callingServerConfiguration = new CallingServerClientSettings();
            clientSettingsDelegate(callingServerConfiguration);

            AddServices(services, callingServerConfiguration.ConnectionString, pmaEndpoint);

            return services;
        }

        private static void AddServices(IServiceCollection services, string connectionString, string? pmaEndpoint)
        {
            services.AddSingleton(pmaEndpoint is not null
                ? new CallingServerClient(new Uri(pmaEndpoint), connectionString)
                : new CallingServerClient(connectionString));
        }
    }
}