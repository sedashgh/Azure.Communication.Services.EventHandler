// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using Azure.Communication.CallAutomation;
using CallAutomation.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace JasonShave.Azure.Communication.Service.EventHandler.CallAutomation;

public static class EventHandlerBuilderExtensions
{
    public static EventHandlerBuilder AddCallAutomationEventHandling(this EventHandlerBuilder eventHandlerBuilder)
    {
        var catalog = new EventCatalogService<Calling>();
        catalog
            .Register<IncomingCall>()
            .Register<CallConnected>()
            .Register<CallDisconnected>()
            .Register<CallTransferAccepted>()
            .Register<CallTransferFailed>()
            .Register<AddParticipantsSucceeded>()
            .Register<AddParticipantsFailed>()
            .Register<ParticipantsUpdated>()
            .Register<PlayCompleted>()
            .Register<PlayFailed>()
            .Register<CallRecordingStateChanged>();
        eventHandlerBuilder.Services.AddSingleton<IEventCatalog<Calling>>(catalog);

        eventHandlerBuilder.Services.AddSingleton<IEventConverter<Calling>, CallAutomationEventConverter>();

        var dispatcher = new CallAutomationEventDispatcher();
        eventHandlerBuilder.Services.AddSingleton<IEventDispatcher<Calling>>(dispatcher);
        eventHandlerBuilder.Services.AddSingleton<ICallAutomationEventSubscriber>(dispatcher);

        return eventHandlerBuilder;
    }
}