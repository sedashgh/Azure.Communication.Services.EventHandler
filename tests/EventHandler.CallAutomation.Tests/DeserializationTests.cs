// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using Azure.Communication.CallAutomation;
using Azure.Messaging;
using FluentAssertions;

namespace JasonShave.Azure.Communication.Service.CallAutomation.Tests;

public class DeserializationTests
{
    [Fact]
    public void CallAutomationEventParser_Parses_CallConnected()
    {
        // arrange
        var eventNamespace = "Microsoft.Communication.CallConnected";
        var callConnectionId = Guid.NewGuid().ToString();
        var serverCallId = Guid.NewGuid().ToString();
        var correlationId = Guid.NewGuid().ToString();

        CallConnected? callConnected = CallAutomationModelFactory.CallConnected(callConnectionId, serverCallId, correlationId);
        var cloudEvent = new CloudEvent("foo/source", eventNamespace, callConnected);

        // act
        CallAutomationEventBase result = CallAutomationEventParser.Parse(cloudEvent);

        // assert
        result.Should().NotBeNull();
        result.Should().BeOfType<CallConnected>();
    }
}