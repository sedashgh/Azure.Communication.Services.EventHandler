// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using Azure.Communication.CallingServer;
using JasonShave.Azure.Communication.Service.CallAutomation.Sdk.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace JasonShave.Azure.Communication.Service.EventHandler.CallAutomation;

public static class EventHandlerBuilderExtensions
{
    public static EventHandlerBuilder AddCallAutomationEventHandling(this EventHandlerBuilder eventHandlerBuilder)
    {
        eventHandlerBuilder.Services.AddSingleton<IEventPublisher<Calling>, EventPublisher<Calling>>();

        var catalog = new EventCatalogService<Calling>();

        catalog
            .Register<IncomingCall>()
            .Register<CallConnected>()
            .Register<CallDisconnected>()
            .Register<CallTransferAccepted>()
            .Register<CallTransferFailed>()
            .Register<AddParticipantsSucceeded>()
            .Register<AddParticipantsFailed>()
            .Register<ParticipantsUpdated>();

        eventHandlerBuilder.Services.AddSingleton<IEventCatalog<Calling>>(catalog);

        var dispatcher = new CallAutomationEventDispatcher();
        eventHandlerBuilder.Services.AddSingleton<IEventDispatcher<Calling>>(dispatcher);
        eventHandlerBuilder.Services.AddSingleton<ICallAutomationEventSubscriber>(dispatcher);

        return eventHandlerBuilder;
    }
}