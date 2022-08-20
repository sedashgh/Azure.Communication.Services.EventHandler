// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using AutoFixture;
using Azure.Messaging;
using JasonShave.Azure.Communication.Service.EventHandler.Tests.Common;
using Microsoft.Extensions.Logging;
using Moq;

namespace JasonShave.Azure.Communication.Service.EventHandler.Tests;

public class EventPublisherTests
{
    [Fact(DisplayName = "Interaction publisher works")]
    public void EventPublisher_JsonPayload_ShouldPublish()
    {
        // arrange
        var fixture = new Fixture();
        var startEvent = fixture.Create<StartEvent>();
        var startEventJson = JsonSerializer.Serialize(startEvent);

        var mockEventConverter = new Mock<IEventConverter<Testing>>();
        var mockEventDispatcher = new Mock<IEventDispatcher<Testing>>();
        var mockLogger = new Mock<ILogger<EventPublisher<Testing>>>();

        mockEventConverter.Setup(c => c.Convert(It.IsAny<string>(), It.IsAny<String>())).Returns(startEvent);
        mockEventDispatcher.Setup(d => d.Dispatch(It.IsAny<object>(), It.IsAny<string>()));

        var subject = new EventPublisher<Testing>(
            mockLogger.Object,
            mockEventDispatcher.Object,
            mockEventConverter.Object);

        // act
        subject.Publish(startEventJson, nameof(StartEvent), "test");

        // assert
        mockEventConverter.Verify(x => x.Convert(It.IsAny<string>(), It.IsAny<String>()), Times.Once);
        mockEventDispatcher.Verify(x => x.Dispatch(It.IsAny<object>(), It.IsAny<string>()), Times.Once);
    }

    [Fact(DisplayName = "A start event wrapped in a CloudEvent can be published")]
    public void EventPublisher_CloudEvent_ShouldPublish()
    {
        // arrange
        var fixture = new Fixture();
        var startEvent = fixture.Create<StartEvent>();
        var cloudEvent = new CloudEvent("testSource", nameof(StartEvent), startEvent);

        var mockEventConverter = new Mock<IEventConverter<Testing>>();
        var mockEventDispatcher = new Mock<IEventDispatcher<Testing>>();
        var mockLogger = new Mock<ILogger<EventPublisher<Testing>>>();

        mockEventConverter.Setup(c => c.Convert(It.IsAny<CloudEvent>())).Returns(startEvent);
        mockEventDispatcher.Setup(d => d.Dispatch(It.IsAny<object>(), It.IsAny<string>()));

        IEventPublisher<Testing> subject = new EventPublisher<Testing>(
            mockLogger.Object,
            mockEventDispatcher.Object,
            mockEventConverter.Object);

        // act
        subject.Publish(cloudEvent);

        // assert
        mockEventDispatcher.Verify(x => x.Dispatch(It.IsAny<object>(), It.IsAny<string>()), Times.Once);
    }
}