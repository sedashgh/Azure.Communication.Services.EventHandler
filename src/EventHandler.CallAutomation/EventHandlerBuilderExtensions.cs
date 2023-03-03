// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.Communication.CallAutomation;
using CallAutomation.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace JasonShave.Azure.Communication.Service.EventHandler.CallAutomation;

public static class EventHandlerBuilderExtensions
{
    public static EventHandlerBuilder AddCallAutomationEventHandling(this EventHandlerBuilder eventHandlerBuilder)
    {
        AddCommon(eventHandlerBuilder);

        return eventHandlerBuilder;
    }

    public static EventHandlerBuilder AddCallAutomationEventHandling(this EventHandlerBuilder eventHandlerBuilder, Action<JsonSerializerOptions> serializerOptions)
    {
        var jsonSerializerOptions = new JsonSerializerOptions();
        serializerOptions(jsonSerializerOptions);

        AddCommon(eventHandlerBuilder, jsonSerializerOptions);

        return eventHandlerBuilder;
    }

    private static void AddCommon(EventHandlerBuilder eventHandlerBuilder, JsonSerializerOptions? jsonSerializerOptions = null)
    {
        var dispatcher = new CallAutomationEventDispatcher();
        eventHandlerBuilder.Services.AddSingleton<IEventDispatcher<Calling>>(dispatcher);
        eventHandlerBuilder.Services.AddSingleton<ICallAutomationEventSubscriber>(dispatcher);

        var catalog = new EventCatalogService<Calling>();
        catalog
            .Register<IncomingCall>()
            .Register<CallConnected>()
            .Register<CallDisconnected>()
            .Register<CallTransferAccepted>()
            .Register<CallTransferFailed>()
            .Register<AddParticipantSucceeded>()
            .Register<AddParticipantFailed>()
            .Register<ParticipantsUpdated>()
            .Register<PlayCompleted>()
            .Register<PlayFailed>()
            .Register<RecognizeCompleted>()
            .Register<RecognizeFailed>()
            .Register<RecordingStateChanged>();
        eventHandlerBuilder.Services.AddSingleton<IEventCatalog<Calling>>(catalog);

        if (jsonSerializerOptions is not null)
        {
            eventHandlerBuilder.Services.AddSingleton<IEventConverter<Calling>>(new CallAutomationEventConverter(catalog, jsonSerializerOptions));
        }
        else
        {
            eventHandlerBuilder.Services.AddSingleton<IEventConverter<Calling>, CallAutomationEventConverter>();
        }
    }
}