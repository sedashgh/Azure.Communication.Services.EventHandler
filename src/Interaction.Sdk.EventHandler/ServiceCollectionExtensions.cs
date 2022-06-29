using JasonShave.Azure.Communication.Service.Interaction.Sdk.Contracts.V2022_11_1.Events;
using JasonShave.Azure.Communication.Service.Interaction.Sdk.EventHandler.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace JasonShave.Azure.Communication.Service.Interaction.Sdk.EventHandler
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInteractionControllerServices(
            this IServiceCollection services)
        {
            // non-version specific
            services.AddSingleton<IEventConverter, JsonEventConverter>();
            services.AddSingleton<IInteractionEventPublisher, InteractionEventPublisher>();

            // version 2022-11-1 services
            var catalog = new EventCatalogService();
            catalog
                .Register<IncomingCall>()
                .Register<CallConnectedEvent>()
                .Register<CallDisconnectedEvent>()
                .Register<CallConnectionStateChanged>();

            services.AddSingleton<IEventCatalog>(catalog);
            
            var dispatcher = new InteractionEventDispatcher();
            services.AddSingleton<IEventDispatcher>(dispatcher);
            services.AddSingleton<IInteractionEventSubscriber>(dispatcher);

            return services;
        }
    }
}
