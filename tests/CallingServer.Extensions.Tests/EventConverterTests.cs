using AutoFixture;
using FluentAssertions;
using JasonShave.Azure.Communication.Service.CallingServer.EventHandler;
using System.Text.Json;

namespace CallingServer.Extensions.Tests;

public class EventConverterTests
{
    [Fact(DisplayName = "JSON converter should deserialize")]
    public void Should_Deserialize()
    {
        // arrange
        var fixture = new Fixture();
        var startEvent = fixture.Create<StartEvent>();
        var startEventJson = JsonSerializer.Serialize(startEvent);

        var subject = new JsonEventConverter();

        // act
        var result = subject.Convert(startEventJson, typeof(StartEvent));

        // assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(startEvent);
    }
}