// Copyright (c) 2022 Jason Shave. All rights reserved. 
//  Licensed under the MIT License.

using Azure.Messaging;
using Azure.Messaging.EventGrid;

namespace JasonShave.Azure.Communication.Service.EventHandler;

public interface IEventConverter<TPrimitive>
    where TPrimitive : IPrimitive
{
    /// <summary>
    /// Converts a <see cref="string"/> payload together with the name of event and returns an <see cref="object"/>.
    /// </summary>
    /// <param name="stringPayload"></param>
    /// <param name="eventName"></param>
    /// <returns>
    /// <see cref="object"/>
    /// </returns>
    object? Convert(string? stringPayload, string eventName);

    /// <summary>
    /// Converts a <see cref="string"/> payload together with the <see cref="Type"/> of event and returns an <see cref="object"/>.
    /// </summary>
    /// <param name="eventPayload"></param>
    /// <param name="eventType"></param>
    /// <returns></returns>
    object? Convert(string? eventPayload, Type eventType);

    /// <summary>
    /// Converts a <see cref="CloudEvent"/> into an <see cref="object"/>.
    /// </summary>
    /// <param name="cloudEvent"></param>
    /// <returns></returns>
    object? Convert(CloudEvent @event);

    ///// <summary>
    ///// Converts a <see cref="EventGridEvent"/> into an <see cref="object"/>.
    ///// </summary>
    ///// <param name="event"></param>
    ///// <returns></returns>
    //object? Convert(EventGridEvent @event);
}