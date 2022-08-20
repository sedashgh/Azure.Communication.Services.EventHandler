// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

namespace JasonShave.Azure.Communication.Service.EventHandler;

public interface IEventCatalog<TPrimitive>
    where TPrimitive : IPrimitive
{
    /// <summary>
    /// Adds an event of type <see cref="TEvent"/> to the catalog.
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    /// <returns>Returns the <see cref="IEventCatalog{TPrimitive}"/> used to chain methods together.</returns>
    IEventCatalog<TPrimitive> Register<TEvent>();

    /// <summary>
    /// Gets the event <see cref="Type"/> by name.
    /// </summary>
    /// <param name="eventName"></param>
    /// <returns>The <see cref="Type"/> of the event given the name.</returns>
    /// <exception cref="ApplicationException">Throws when not found.</exception>
    Type Get(string eventName);

    /// <summary>
    /// Get event <see cref="Type"/> by name.
    /// </summary>
    /// <param name="eventType"></param>
    /// <returns>The <see cref="string"/> name of the event.</returns>
    /// <exception cref="ApplicationException">Throws when not found.</exception>
    string Get(Type eventType);

    IEnumerable<Type> List();
}