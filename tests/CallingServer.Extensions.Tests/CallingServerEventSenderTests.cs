using AutoFixture;
using JasonShave.Azure.Communication.Service.CallingServer.Extensions;
using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Interfaces;
using Moq;
using System.Text.Json;

namespace CallingServer.Extensions.Tests;

public class CallingServerEventSenderTests
{
    [Fact(DisplayName = "Sending works")]
    public void Should_Send()
    {
        // arrange
        var fixture = new Fixture();
        var startEvent = fixture.Create<StartEvent>();
        var startEventJson = JsonSerializer.Serialize(startEvent);

        var mockEventCatalog = new Mock<IEventCatalog>();
        var mockEventConverter = new Mock<IEventConverter>();
        var mockEventDispatcher = new Mock<IEventDispatcher>();

        mockEventCatalog.Setup(c => c.Get(It.IsAny<string>())).Returns(typeof(StartEvent));
        mockEventConverter.Setup(c => c.Convert(It.IsAny<string>(), It.IsAny<Type>())).Returns(startEvent);
        mockEventDispatcher.Setup(d => d.Dispatch(It.IsAny<object>()));

        var subject = new CallingServerEventPublisher(mockEventCatalog.Object, mockEventDispatcher.Object,
            mockEventConverter.Object);

        // act
        subject.Send(startEventJson, nameof(StartEvent));

        // assert
        mockEventCatalog.Verify(x => x.Get(It.IsAny<string>()), Times.Once);
        mockEventConverter.Verify(x => x.Convert(It.IsAny<string>(), It.IsAny<Type>()), Times.Once);
        mockEventDispatcher.Verify(x => x.Dispatch(It.IsAny<object>()), Times.Once);
    }
}