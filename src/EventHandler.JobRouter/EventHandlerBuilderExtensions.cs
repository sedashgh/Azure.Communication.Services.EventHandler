// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using JasonShave.Azure.Communication.Service.JobRouter.Sdk.Contracts.V2021_10_20_preview.Events;
using Microsoft.Extensions.DependencyInjection;

namespace JasonShave.Azure.Communication.Service.EventHandler.JobRouter;

public static class EventHandlerBuilderExtensions
{
    public static EventHandlerBuilder AddJobRouterEventHandling(this EventHandlerBuilder eventHandlerBuilder)
    {
        AddCommon(eventHandlerBuilder);

        return eventHandlerBuilder;
    }

    public static EventHandlerBuilder AddJobRouterEventHandling(this EventHandlerBuilder eventHandlerBuilder, Action<JsonSerializerOptions> serializerOptions)
    {
        var jsonSerializerOptions = new JsonSerializerOptions();
        serializerOptions(jsonSerializerOptions);

        AddCommon(eventHandlerBuilder, jsonSerializerOptions);

        return eventHandlerBuilder;
    }

    private static void AddCommon(EventHandlerBuilder eventHandlerBuilder, JsonSerializerOptions? jsonSerializerOptions = null)
    {
        var dispatcher = new JobRouterEventDispatcher();
        eventHandlerBuilder.Services.AddSingleton<IEventDispatcher<Router>>(dispatcher);
        eventHandlerBuilder.Services.AddSingleton<IJobRouterEventSubscriber>(dispatcher);

        var catalog = new EventCatalogService<Router>();
        catalog
            .Register<RouterJobCancelled>()
            .Register<RouterJobClassificationFailed>()
            .Register<RouterJobClassified>()
            .Register<RouterJobClosed>()
            .Register<RouterJobCompleted>()
            .Register<RouterJobExceptionTriggered>()
            .Register<RouterJobQueued>()
            .Register<RouterJobReceived>()
            .Register<RouterJobUnassigned>()
            .Register<RouterJobWorkerSelectorsExpired>()
            .Register<RouterWorkerDeregistered>()
            .Register<RouterWorkerRegistered>()
            .Register<RouterWorkerOfferAccepted>()
            .Register<RouterWorkerOfferDeclined>()
            .Register<RouterWorkerOfferExpired>()
            .Register<RouterWorkerOfferIssued>()
            .Register<RouterWorkerOfferRevoked>();
        eventHandlerBuilder.Services.AddSingleton<IEventCatalog<Router>>(catalog);

        if (jsonSerializerOptions is not null)
        {
            eventHandlerBuilder.Services.AddSingleton<IEventConverter<Router>>(new JobRouterEventConverter(catalog, jsonSerializerOptions));
        }
        else
        {
            eventHandlerBuilder.Services.AddSingleton<IEventConverter<Router>, JobRouterEventConverter>();
        }
    }
}