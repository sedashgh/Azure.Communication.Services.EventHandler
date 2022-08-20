// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

namespace JasonShave.Azure.Communication.Service.EventHandler;

public interface IEventDispatcher<TPrimitive>
    where TPrimitive : IPrimitive
{
    void Dispatch(object @event, string? contextId = default);
}