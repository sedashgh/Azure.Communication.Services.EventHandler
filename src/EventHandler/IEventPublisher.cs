// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

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
    void Publish(string data, string eventName, string contextId = default!);
}