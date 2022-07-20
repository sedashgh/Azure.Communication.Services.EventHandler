// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using AutoFixture;
using FluentAssertions;
using JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts.V2022_11_1_preview.Events;
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

    [Fact(DisplayName = "Incoming PSTN call can be deserialized")]
    public void EventConverter_Should_DeserializeIncomingCall()
    {
        // arrange
        var incomingCall =
            "{\"to\": {\"rawId\": \"4:\u002B18005551212\",\"phoneNumber\": {\"value\": \"\u002B18005551212\"}},\"from\": {\"rawId\": \"4:\u002B14255551212\",\"phoneNumber\": {\"value\": \"\u002B14255551212\"}},\"hasIncomingVideo\": false,\"callerDisplayName\": \"\",\"incomingCallContext\": \"some_really_long_string\",\"correlationId\": \"94ec3b97-a505-491b-9576-e3bb3d9cd084\"}";
        var subject = new JsonEventConverter(new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });

        // act
        var result = (IncomingCall)subject.Convert(incomingCall, typeof(IncomingCall));

        // assert
        result.Should().NotBeNull();
        result.From.Should().NotBeNull();
        result.IncomingCallContext.Should().NotBeNull();
        result.From.PhoneNumber.Value.Should().NotBeNull();
        result.To.PhoneNumber.Value.Should().NotBeNull();
        result.CorrelationId.Should().NotBeNull();

        // output
        _testOutputHelper.WriteLine($"From: {result.From.RawId}");
        _testOutputHelper.WriteLine($"From phone number: {result.From.PhoneNumber.Value}");
        _testOutputHelper.WriteLine($"To: {result.To.RawId}");
        _testOutputHelper.WriteLine($"To phone number: {result.To.PhoneNumber.Value}");
        _testOutputHelper.WriteLine($"Context: {result.IncomingCallContext}");
        _testOutputHelper.WriteLine($"CorrelationId: {result.CorrelationId}");
    }
}