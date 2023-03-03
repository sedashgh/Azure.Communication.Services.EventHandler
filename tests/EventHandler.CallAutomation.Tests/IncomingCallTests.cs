﻿// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using CallAutomation.Contracts;
using FluentAssertions;
using JasonShave.Azure.Communication.Service.EventHandler;
using JasonShave.Azure.Communication.Service.EventHandler.CallAutomation;
using Moq;
using Xunit.Abstractions;

namespace JasonShave.Azure.Communication.Service.CallAutomation.Tests;

public class IncomingCallTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public IncomingCallTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact(DisplayName = "Incoming PSTN call can be deserialized")]
    public void EventConverter_Should_DeserializeIncomingCall()
    {
        // arrange
        var incomingCallNamespace = "Microsoft.Communication.IncomingCall";
        var incomingCall =
            "{\"to\": {\"kind\": \"phoneNumber\",\"rawId\": \"4:\u002B18005551212\",\"phoneNumber\": {\"value\": \"\u002B18005551212\"}},\"from\": {\"kind\": \"phoneNumber\", \"rawId\": \"4:\u002B14255551212\",\"phoneNumber\": {\"value\": \"\u002B14255551212\"}},\"hasIncomingVideo\": false,\"callerDisplayName\": \"\",\"incomingCallContext\": \"some_really_long_string\",\"correlationId\": \"94ec3b97-a505-491b-9576-e3bb3d9cd084\"}";
        var mockEventCatalog = new Mock<IEventCatalog<Calling>>();
        mockEventCatalog.Setup(x => x.Get(It.IsAny<Type>())).Returns(incomingCallNamespace);

        var subject = new CallAutomationEventConverter(mockEventCatalog.Object, null);

        // act
        var result = (IncomingCall)subject.Convert(incomingCall, incomingCallNamespace);

        // assert
        result.Should().NotBeNull();
        result.From.Should().NotBeNull();
        result.IncomingCallContext.Should().NotBeNull();
        result.From.RawId.Should().NotBeNull();
        result.To.RawId.Should().NotBeNull();
        result.CorrelationId.Should().NotBeNull();

        // output
        _testOutputHelper.WriteLine($"From: {result.From.RawId}");
        _testOutputHelper.WriteLine($"From phone number: {result.From.RawId}");
        _testOutputHelper.WriteLine($"From kind: {result.From.Kind}");
        _testOutputHelper.WriteLine($"To: {result.To.RawId}");
        _testOutputHelper.WriteLine($"To phone number: {result.To.RawId}");
        _testOutputHelper.WriteLine($"To kind: {result.To.Kind}");
        _testOutputHelper.WriteLine($"Context: {result.IncomingCallContext}");
        _testOutputHelper.WriteLine($"CorrelationId: {result.CorrelationId}");
    }
}