// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using AutoFixture;
using Azure.Communication.CallAutomation;
using CallAutomation.Contracts;
using FluentAssertions;
using JasonShave.Azure.Communication.Service.EventHandler.CallAutomation;
using CommunicationUserIdentifier = Azure.Communication.CommunicationUserIdentifier;

namespace JasonShave.Azure.Communication.Service.CallAutomation.Tests;

public class CallAutomationEventDispatcherTests
{
    [Fact(DisplayName = "Dispatch should invoke event handler")]
    public void Should_Invoke_Handler()
    {
        // arrange
        var testId = Guid.NewGuid().ToString();
        var fixture = new Fixture();
        var incomingCall = fixture.Create<IncomingCall>();

        var callConnectionId = Guid.NewGuid().ToString();
        var serverCallId = Guid.NewGuid().ToString();
        var correlationId = Guid.NewGuid().ToString();
        var participants = fixture.CreateMany<CommunicationUserIdentifier>();

        var callConnectedEvent = CallAutomationModelFactory.CallConnected(callConnectionId, serverCallId, correlationId);

        var callDisconnectedEvent = CallAutomationModelFactory.CallDisconnected(callConnectionId, serverCallId, correlationId);
        var addParticipantSucceededEvent = CallAutomationModelFactory.AddParticipantsSucceeded(callConnectionId, serverCallId, correlationId, null, null, participants);
        var addParticipantFailedEvent = CallAutomationModelFactory.AddParticipantsFailed(callConnectionId, serverCallId, correlationId, null, null, participants);
        var callTransferAcceptedEvent = CallAutomationModelFactory.CallTransferAccepted(callConnectionId, serverCallId, correlationId);
        var callTransferFailedEvent = CallAutomationModelFactory.CallTransferFailed(callConnectionId, serverCallId, correlationId);
        var participantUpdatedEvent = CallAutomationModelFactory.ParticipantsUpdated(callConnectionId, serverCallId, correlationId);

        var subject = new CallAutomationEventDispatcher();

        subject.OnCallConnected += (@event, contextId) =>
        {
            @event.Should().NotBeNull();
            @event.Should().BeOfType<CallConnected>();
            @event.Should().BeEquivalentTo(callConnectedEvent);
            contextId.Should().BeSameAs(testId);
            contextId.Should().NotBeNullOrEmpty();
            return ValueTask.CompletedTask;
        };

        subject.OnCallDisconnected += (@event, contextId) =>
        {
            @event.Should().NotBeNull();
            @event.Should().BeOfType<CallDisconnected>();
            @event.Should().BeEquivalentTo(callDisconnectedEvent);
            contextId.Should().BeSameAs(testId);
            contextId.Should().NotBeNullOrEmpty();
            return ValueTask.CompletedTask;
        };

        subject.OnIncomingCall += (@event, contextId) =>
        {
            @event.Should().NotBeNull();
            @event.Should().BeOfType<IncomingCall>();
            contextId.Should().BeNullOrEmpty();
            return ValueTask.CompletedTask;
        };

        subject.OnAddParticipantsSucceeded += (@event, contextId) =>
        {
            @event.Should().NotBeNull();
            @event.Should().BeOfType<AddParticipantsSucceeded>();
            contextId.Should().BeNullOrEmpty();
            return ValueTask.CompletedTask;
        };

        subject.OnAddParticipantsFailed += (@event, contextId) =>
        {
            @event.Should().NotBeNull();
            @event.Should().BeOfType<AddParticipantsFailed>();
            contextId.Should().BeNullOrEmpty();
            return ValueTask.CompletedTask;
        };

        subject.OnCallTransferAccepted += (@event, contextId) =>
        {
            @event.Should().NotBeNull();
            @event.Should().BeOfType<CallTransferAccepted>();
            contextId.Should().BeNullOrEmpty();
            return ValueTask.CompletedTask;
        };

        subject.OnCallTransferFailed += (@event, contextId) =>
        {
            @event.Should().NotBeNull();
            @event.Should().BeOfType<CallTransferFailed>();
            contextId.Should().BeNullOrEmpty();
            return ValueTask.CompletedTask;
        };

        subject.OnParticipantsUpdated += (@event, contextId) =>
        {
            @event.Should().NotBeNull();
            @event.Should().BeOfType<ParticipantsUpdated>();
            contextId.Should().BeNullOrEmpty();
            return ValueTask.CompletedTask;
        };

        // act
        subject.Dispatch(callConnectedEvent, testId);
        subject.Dispatch(callDisconnectedEvent, testId);
        subject.Dispatch(incomingCall);
        subject.Dispatch(addParticipantSucceededEvent);
        subject.Dispatch(addParticipantFailedEvent);
        subject.Dispatch(callTransferAcceptedEvent);
        subject.Dispatch(callTransferFailedEvent);
        subject.Dispatch(participantUpdatedEvent);
    }

    [Fact(DisplayName = "Null context should invoke")]
    public void EventDispatcher_NullContext_ShouldInvoke()
    {
        // arrange
        var fixture = new Fixture();
        var incomingCall = fixture.Create<IncomingCall>();
        var subject = new CallAutomationEventDispatcher();

        // act
        subject.OnIncomingCall += HandleIncomingCall;

        ValueTask HandleIncomingCall(IncomingCall @event, string? contextId)
        {
            @event.Should().NotBeNull();
            contextId.Should().BeNull();
            return ValueTask.CompletedTask;
        }

        // assert
        subject.Dispatch(incomingCall);

        // clean up
        subject.OnIncomingCall -= HandleIncomingCall;
    }

    [Fact(DisplayName = "EventDispatcher with no subscriber should not throw null ref")]
    public void EventDispatcher_NullSubscriber_DoesNotThrow()
    {
        // arrange
        var fixture = new Fixture();
        var incomingCall = fixture.Create<IncomingCall>();
        var subject = new CallAutomationEventDispatcher();

        // act
        subject.OnIncomingCall += HandleIncomingCall;

        ValueTask HandleIncomingCall(IncomingCall @event, string? contextId)
        {
            return ValueTask.CompletedTask;
        }

        // assert
        subject.Invoking(x => x.Dispatch(incomingCall)).Should().NotThrow();

        // clean up
        subject.OnIncomingCall -= HandleIncomingCall;
    }
}