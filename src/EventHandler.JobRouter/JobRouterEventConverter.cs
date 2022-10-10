// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Messaging;

namespace JasonShave.Azure.Communication.Service.EventHandler.JobRouter;

internal class JobRouterEventConverter : IEventConverter<Router>
{
    private readonly IEventCatalog<Router> _routerEventCatalog;
    private readonly JsonSerializerOptions _serializerOptions;

    public JobRouterEventConverter(
        IEventCatalog<Router> routerEventCatalog,
        JsonSerializerOptions? serializerOptions = null)
    {
        _routerEventCatalog = routerEventCatalog;
        _serializerOptions = serializerOptions ?? new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public object? Convert(string? stringPayload, string eventName)
    {
        var eventType = _routerEventCatalog.Get(eventName);
        return Convert(stringPayload, eventType);
    }

    public object? Convert(string? eventPayload, Type eventType)
    {
        if (eventPayload is null)
            throw new ArgumentNullException(nameof(eventPayload));

        var result = JsonSerializer.Deserialize(eventPayload, eventType, _serializerOptions);

        if (result is null)
            throw new ApplicationException($"Unable to convert type {eventType.Name}");

        return result;
    }

    public object? Convert(CloudEvent @event) => Convert(@event.Data?.ToString(), @event.Type);
}