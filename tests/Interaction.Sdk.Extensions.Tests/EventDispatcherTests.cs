using AutoFixture;
using FluentAssertions;
using JasonShave.Azure.Communication.Service.Interaction.Sdk.Contracts.V2022_11_1.Events;
using JasonShave.Azure.Communication.Service.Interaction.Sdk.EventHandler;

namespace Interaction.Sdk.Tests;

public class EventDispatcherTests
{
    [Fact(DisplayName = "Dispatch should invoke event handler")]
    public void Should_Invoke_Handler()
    {
        // arrange
        var testId = Guid.NewGuid().ToString();
        var fixture = new Fixture();
        var callConnectedEvent = fixture.Create<CallConnectedEvent>();
        var callDisconnectedEvent = fixture.Create<CallDisconnectedEvent>();
        var callConnectionStateChangedEvent = fixture.Create<CallConnectionStateChanged>();
        var incomingCall = fixture.Create<IncomingCall>();

        var subject = new InteractionEventDispatcher();

        subject.OnCallConnected += async (@event, contextId) =>
        {
            @event.Should().NotBeNull();
            @event.Should().BeOfType<CallConnectedEvent>();
            @event.Should().BeEquivalentTo(callConnectedEvent);
            contextId.Should().NotBeNullOrEmpty();
        };

        subject.OnCallDisconnected += async (@event, contextId) =>
        {
            @event.Should().NotBeNull();
            @event.Should().BeOfType<CallDisconnectedEvent>();
            @event.Should().BeEquivalentTo(callDisconnectedEvent);
            contextId.Should().NotBeNullOrEmpty();
        };

        subject.OnCallConnectionStateChanged += async (@event, contextId) =>
        {
            @event.Should().NotBeNull();
            @event.Should().BeOfType<CallConnectionStateChanged>();
            @event.Should().BeEquivalentTo(callConnectionStateChangedEvent);
            contextId.Should().NotBeNullOrEmpty();
        };

        subject.OnIncomingCall += async (@event, contextId) =>
        {
            @event.Should().NotBeNull();
            @event.Should().BeOfType<IncomingCall>();
            contextId.Should().NotBeNullOrEmpty();
        };

        // act
        subject.Dispatch(callConnectedEvent, typeof(CallConnectedEvent), testId);
        subject.Dispatch(callDisconnectedEvent, typeof(CallDisconnectedEvent), testId);
        subject.Dispatch(callConnectionStateChangedEvent, typeof(CallConnectionStateChanged), testId);
        subject.Dispatch(incomingCall, typeof(IncomingCall));
    }
}