// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using FluentAssertions;
using JasonShave.Azure.Communication.Service.EventHandler.Tests.Common;

namespace JasonShave.Azure.Communication.Service.EventHandler.Tests
{
    public class EventCatalogTests
    {
        [Fact(DisplayName = "Registering event type returns same type by name")]
        public void EventCatalogService_Returns_Registered_Types()
        {
            // arrange
            var subject = new EventCatalogService<Testing>();

            // act
            subject
                .Register<StartEvent>()
                .Register<StopEvent>();

            var startEvent = subject.Get(nameof(StartEvent));
            var stopEvent = subject.Get(nameof(StopEvent));

            // assert
            startEvent.Should().NotBeNull();
            startEvent.Should().BeAssignableTo<StartEvent>();

            stopEvent.Should().NotBeNull();
            stopEvent.Should().BeAssignableTo<StopEvent>();
        }

        [Fact(DisplayName = "Handle fully qualified event namespaces")]
        public void EventCatalogService_Handles_FullyQualifiedEvents()
        {
            // arrange
            var subject = new EventCatalogService<Testing>();

            // act
            subject.Register<StartEvent>();

            var startEvent = subject.Get($"Microsoft.Communication.{nameof(StartEvent)}");

            // assert
            startEvent.Should().NotBeNull();
            startEvent.Should().BeAssignableTo<StartEvent>();
        }
    }
}