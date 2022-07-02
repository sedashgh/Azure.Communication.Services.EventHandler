using JasonShave.Azure.Communication.Service.EventHandler.Abstractions;
using JasonShave.Azure.Communication.Service.EventHandler.Abstractions.Interfaces;
using JasonShave.Azure.Communication.Service.Interaction.Sdk.Contracts.V2022_11_1_preview.Events;
using Microsoft.Extensions.DependencyInjection;

namespace JasonShave.Azure.Communication.Service.Interaction.Sdk.EventHandler;

public static class EventHandlerBuilderExtensions
{
    public static EventHandlerBuilder AddInteractionEventHandling(this EventHandlerBuilder eventHandlerBuilder)
    {
        eventHandlerBuilder.Services.AddSingleton<IEventPublisher<Interaction>, EventPublisher<Interaction>>();

        var catalog = new EventCatalogService<Interaction>();

        catalog
            .Register<IncomingCall>()
            .Register<CallConnectedEvent>()
            .Register<CallDisconnectedEvent>()
            .Register<CallConnectionStateChanged>();

        eventHandlerBuilder.Services.AddSingleton<IEventCatalog<Interaction>>(catalog);

        var dispatcher = new InteractionEventDispatcher();
        eventHandlerBuilder.Services.AddSingleton<IEventDispatcher<Interaction>>(dispatcher);
        eventHandlerBuilder.Services.AddSingleton<IInteractionEventSubscriber>(dispatcher);

        return eventHandlerBuilder;
    }
}