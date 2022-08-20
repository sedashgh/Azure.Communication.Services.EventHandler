// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace JasonShave.Azure.Communication.Service.EventHandler;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static EventHandlerBuilder AddEventHandlerServices(this IServiceCollection services)
    {
        services.AddSingleton(typeof(IEventPublisher<>), typeof(EventPublisher<>));
        return new(services);
    }
}