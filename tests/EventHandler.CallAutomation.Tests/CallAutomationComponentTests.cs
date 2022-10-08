// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using Azure.Communication.CallAutomation;
using Azure.Messaging;
using JasonShave.Azure.Communication.Service.EventHandler;
using JasonShave.Azure.Communication.Service.EventHandler.CallAutomation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace JasonShave.Azure.Communication.Service.CallAutomation.Tests;

public class CallAutomationComponentTests
{
    private readonly IEventPublisher<Calling> _callingEventPublisher;
    private readonly ICallAutomationEventSubscriber _callAutomationEventSubscriber;
    private readonly string _callConnectionId;
    private readonly string _serverCallId;
    private readonly string _correlationId;
    private readonly string _contextId;

    public CallAutomationComponentTests()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddEventHandlerServices().AddCallAutomationEventHandling();
            })
            .Build();

        _callingEventPublisher = host.Services.GetService<IEventPublisher<Calling>>();
        _callAutomationEventSubscriber = host.Services.GetService<ICallAutomationEventSubscriber>();

        _callConnectionId = Guid.NewGuid().ToString();
        _serverCallId = Guid.NewGuid().ToString();
        _correlationId = Guid.NewGuid().ToString();
        _contextId = Guid.NewGuid().ToString();
    }

    [Fact(DisplayName = "CallConnected event should be dispatched to listener")]
    public void CallConnected_Should_Dispatch()
    {
        // arrange
        var eventNamespace = "Microsoft.Communication.CallConnected";

        CallConnected? callConnected = CallAutomationModelFactory.CallConnected(_callConnectionId, _serverCallId, _correlationId);
        var cloudEvent = new CloudEvent(
            "some source",
            eventNamespace,
            new BinaryData(callConnected, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
            "application/json");

        _callAutomationEventSubscriber.OnCallConnected += HandleOnCallConnected;

        // act
        _callingEventPublisher.Publish(cloudEvent, _contextId);
    }

    private ValueTask HandleOnCallConnected(CallConnected callConnected, string? contextId)
    {
        Assert.Equal(_callConnectionId, callConnected.CallConnectionId);
        Assert.Equal(_serverCallId, callConnected.ServerCallId);
        Assert.Equal(_correlationId, callConnected.CorrelationId);
        Assert.Equal(_contextId, contextId);
        return ValueTask.CompletedTask;
    }
}