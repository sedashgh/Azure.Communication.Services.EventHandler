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

        [Fact(DisplayName = "Missing event throws")]
        public void EventCatalogService_MissingType_Throws()
        {
            // arrange
            var subject = new EventCatalogService<Testing>();

            // act/assert
            subject.Invoking(x => x.Get(nameof(StartEvent))).Should().Throw<ApplicationException>();
        }

        [Fact(DisplayName = "Event catalog lists events")]
        public void EventCatalogService_List_ReturnsMany()
        {
            // arrange
            var subject = new EventCatalogService<Testing>();
            subject
                .Register<StartEvent>()
                .Register<StopEvent>();

            // act
            var result = subject.List();

            // assert
            result.Should().HaveCount(2);
        }
    }
}