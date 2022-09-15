// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Extensions.DependencyInjection;

namespace JasonShave.Azure.Communication.Service.EventHandler;

public sealed class EventHandlerBuilder
{
    public IServiceCollection Services { get; }

    public EventHandlerBuilder(IServiceCollection services) => Services = services;
}