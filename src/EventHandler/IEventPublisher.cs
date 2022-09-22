// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using Azure.Messaging;
using Azure.Messaging.EventGrid;

namespace JasonShave.Azure.Communication.Service.EventHandler;

public interface IEventPublisher<TPrimitive>
    where TPrimitive : IPrimitive
{
    /// <summary>
    /// Used to publish data for event subscription.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="eventName"></param>
    /// <param name="contextId"></param>
    /// <exception cref="InvalidOperationException"></exception>
    void Publish(string data, string eventName, string? contextId = default);

    /// <summary>
    /// Used to publish a data array for event subscription.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="eventName"></param>
    /// <param name="contextId"></param>
    /// <exception cref="InvalidOperationException"></exception>
    void Publish(string[] data, string eventName, string? contextId = default);

    /// <summary>
    /// Used to publish a <see cref="CloudEvent"/> typically received from a callback or when
    /// this schema type is specified using Azure Event Grid.
    /// </summary>
    /// <param name="cloudEvent"></param>
    /// <param name="contextId"></param>
    void Publish(CloudEvent cloudEvent, string? contextId = default);

    /// <summary>
    /// Used to publish many <see cref="CloudEvent"/> typically received from a callback or when
    /// this schema type is specified using Azure Event Grid.
    /// </summary>
    /// <param name="cloudEvents"></param>
    /// <param name="contextId"></param>
    void Publish(CloudEvent[] cloudEvents, string? contextId = default);

    /// <summary>
    /// Used to publish a <see cref="EventGridEvent"/> when receiving data from an Event Grid subscription.
    /// </summary>
    /// <param name="eventGridEvent"></param>
    /// <param name="contextId"></param>
    void Publish(EventGridEvent eventGridEvent, string? contextId = default);

    /// <summary>
    /// Used to publish many <see cref="EventGridEvent"/> when receiving data from an Event Grid subscription.
    /// </summary>
    /// <param name="eventGridEvents"></param>
    /// <param name="contextId"></param>
    void Publish(EventGridEvent[] eventGridEvents, string? contextId = default);
}