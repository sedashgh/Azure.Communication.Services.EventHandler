// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using Azure.Communication.CallingServer;
using JasonShave.Azure.Communication.Service.CallAutomation.Sdk.Contracts;

namespace JasonShave.Azure.Communication.Service.EventHandler.CallAutomation;

internal class CallAutomationEventDispatcher : IEventDispatcher<Calling>, ICallAutomationEventSubscriber
{
    public event Func<IncomingCall, string?, ValueTask>? OnIncomingCall;
    public event Func<CallConnected, string?, ValueTask>? OnCallConnected;
    public event Func<CallDisconnected, string?, ValueTask>? OnCallDisconnected;
    public event Func<AddParticipantsSucceeded, string?, ValueTask>? OnAddParticipantsSucceeded;
    public event Func<AddParticipantsFailed, string?, ValueTask>? OnAddParticipantsFailed;
    public event Func<CallTransferAccepted, string?, ValueTask>? OnCallTransferAccepted;
    public event Func<CallTransferFailed, string?, ValueTask>? OnCallTransferFailed;
    public event Func<ParticipantsUpdated, string?, ValueTask>? OnParticipantsUpdated;

    private readonly Dictionary<Type, Func<object, string?, ValueTask>> _eventDictionary;

    public CallAutomationEventDispatcher()
    {
        _eventDictionary = new Dictionary<Type, Func<object, string?, ValueTask>>
        {
            [typeof(IncomingCall)] = async (@event, contextId) =>
            {
                if (OnIncomingCall is null) return;
                await OnIncomingCall.Invoke((IncomingCall)@event, contextId);
            },
            [typeof(CallConnected)] = async (@event, contextId) =>
            {
                if (OnCallConnected is null) return;
                await OnCallConnected.Invoke((CallConnected)@event, contextId);
            },
            [typeof(CallDisconnected)] = async (@event, contextId) =>
            {
                if (OnCallDisconnected is null) return;
                await OnCallDisconnected.Invoke((CallDisconnected)@event, contextId);
            },
            [typeof(AddParticipantsSucceeded)] = async (@event, contextId) =>
            {
                if (OnAddParticipantsSucceeded is null) return;
                await OnAddParticipantsSucceeded.Invoke((AddParticipantsSucceeded)@event, contextId);
            },
            [typeof(AddParticipantsFailed)] = async (@event, contextId) =>
            {
                if (OnAddParticipantsFailed is null) return;
                await OnAddParticipantsFailed.Invoke((AddParticipantsFailed)@event, contextId);
            },
            [typeof(CallTransferAccepted)] = async (@event, contextId) =>
            {
                if (OnCallTransferAccepted is null) return;
                await OnCallTransferAccepted.Invoke((CallTransferAccepted)@event, contextId);
            },
            [typeof(CallTransferFailed)] = async (@event, contextId) =>
            {
                if (OnCallTransferFailed is null) return;
                await OnCallTransferFailed.Invoke((CallTransferFailed)@event, contextId);
            },
            [typeof(ParticipantsUpdated)] = async (@event, contextId) =>
            {
                if (OnParticipantsUpdated is null) return;
                await OnParticipantsUpdated.Invoke((ParticipantsUpdated)@event, contextId);
            },
        };
    }

    public void Dispatch(object @event, Type eventType, string contextId = default!)
    {
        _eventDictionary[eventType](@event, contextId);
    }
}