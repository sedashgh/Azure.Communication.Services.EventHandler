// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using Azure.Communication.CallAutomation;
using Azure.Messaging;
using JasonShave.Azure.Communication.Service.CallAutomation.Sdk.Contracts;
using System.Text.Json;

namespace JasonShave.Azure.Communication.Service.EventHandler.CallAutomation;

/// <inheritdoc />
public sealed class CallAutomationEventConverter : IEventConverter<Calling>
{
    private readonly IEventCatalog<Calling> _eventCatalog;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public CallAutomationEventConverter(IEventCatalog<Calling> eventCatalog, JsonSerializerOptions? jsonSerializerOptions = null)
    {
        _eventCatalog = eventCatalog;
        _jsonSerializerOptions = jsonSerializerOptions ?? new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public object? Convert(string? stringPayload, string eventName)
    {
        if (eventName == "Microsoft.Communication.IncomingCall")
        {
            return JsonSerializer.Deserialize<IncomingCall>(stringPayload, _jsonSerializerOptions);
        }

        CallAutomationEventBase @event = CallAutomationEventParser.Parse(stringPayload, eventName);
        return @event;
    }

    public object? Convert(string? eventPayload, Type eventType)
    {
        var eventName = _eventCatalog.Get(eventType);
        return Convert(eventPayload, eventName);
    }

    public object? Convert(CloudEvent @event)
    {
        if (@event.Type == "Microsoft.Communication.IncomingCall")
        {
            return JsonSerializer.Deserialize<IncomingCall>(@event.Data, _jsonSerializerOptions);
        }

        CallAutomationEventBase result = CallAutomationEventParser.Parse(@event);
        return result;
    }
}