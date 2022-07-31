// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using AutoFixture;
using FluentAssertions;
using JasonShave.Azure.Communication.Service.EventHandler.Tests.Common;
using Xunit.Abstractions;

namespace JasonShave.Azure.Communication.Service.EventHandler.Tests;

public class EventConverterTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public EventConverterTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact(DisplayName = "JSON converter should deserialize")]
    public void Should_Deserialize()
    {
        // arrange
        var fixture = new Fixture();
        var startEvent = fixture.Create<StartEvent>();
        var startEventJson = JsonSerializer.Serialize(startEvent);

        var subject = new JsonEventConverter(new JsonSerializerOptions());

        // act
        var result = subject.Convert(startEventJson, typeof(StartEvent));

        // assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(startEvent);
    }
}