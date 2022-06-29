using System.Text.Json;
using AutoFixture;
using FluentAssertions;
using JasonShave.Azure.Communication.Service.Interaction.Sdk.EventHandler;

namespace Interaction.Sdk.Tests;

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