using AutoFixture;
using JasonShave.Azure.Communication.Service.EventHandler.Abstractions;
using JasonShave.Azure.Communication.Service.EventHandler.Abstractions.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text.Json;

namespace Interaction.Sdk.Tests;

public class InteractionEventPublisherTests
{
    [Fact(DisplayName = "Interaction publisher works")]
    public void Should_Send()
    {
        // arrange
        var fixture = new Fixture();
        var startEvent = fixture.Create<StartEvent>();
        var startEventJson = JsonSerializer.Serialize(startEvent);

        var mockEventCatalog = new Mock<IEventCatalog<JasonShave.Azure.Communication.Service.Interaction.Sdk.EventHandler.Interaction>>();
        var mockEventConverter = new Mock<IEventConverter>();
        var mockEventDispatcher = new Mock<IEventDispatcher<JasonShave.Azure.Communication.Service.Interaction.Sdk.EventHandler.Interaction>>();
        var mockLogger = new Mock<ILogger<EventPublisher<JasonShave.Azure.Communication.Service.Interaction.Sdk.EventHandler.Interaction>>>();

        mockEventCatalog.Setup(c => c.Get(It.IsAny<string>())).Returns(typeof(StartEvent));
        mockEventConverter.Setup(c => c.Convert(It.IsAny<BinaryData>(), It.IsAny<Type>())).Returns(startEvent);
        mockEventDispatcher.Setup(d => d.Dispatch(It.IsAny<object>(), It.IsAny<Type>(), It.IsAny<string>()));

        var subject = new EventPublisher<JasonShave.Azure.Communication.Service.Interaction.Sdk.EventHandler.Interaction>(
            mockLogger.Object,
            mockEventCatalog.Object,
            mockEventDispatcher.Object,
            mockEventConverter.Object);

        // act
        subject.Publish(new BinaryData(startEventJson), nameof(StartEvent), "test");

        // assert
        mockEventCatalog.Verify(x => x.Get(It.IsAny<string>()), Times.Once);
        mockEventConverter.Verify(x => x.Convert(It.IsAny<BinaryData>(), It.IsAny<Type>()), Times.Once);
        mockEventDispatcher.Verify(x => x.Dispatch(It.IsAny<object>(), It.IsAny<Type>(), It.IsAny<string>()), Times.Once);
    }
}