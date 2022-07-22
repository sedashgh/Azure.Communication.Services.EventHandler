// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts.V2022_11_1_preview.Events;

namespace JasonShave.Azure.Communication.Service.EventHandler.CallingServer;

internal class CallingServerEventDispatcher : IEventDispatcher<Calling>, ICallingServerEventSubscriber
{
    public event Func<IncomingCall, string?, ValueTask>? OnIncomingCall;
    public event Func<CallConnected, string?, ValueTask>? OnCallConnected;
    public event Func<CallDisconnected, string?, ValueTask>? OnCallDisconnected;
    public event Func<AddParticipantSucceeded, string?, ValueTask>? OnAddParticipantSucceeded;
    public event Func<AddParticipantFailed, string?, ValueTask>? OnAddParticipantFailed;
    public event Func<CallTransferAccepted, string?, ValueTask>? OnCallTransferAccepted;
    public event Func<CallTransferFailed, string?, ValueTask>? OnCallTransferFailed;
    public event Func<RemoveParticipantSucceeded, string?, ValueTask>? OnRemoveParticipantSucceeded;
    public event Func<RemoveParticipantFailed, string?, ValueTask>? OnRemoveParticipantFailed;
    public event Func<ParticipantsUpdated, string?, ValueTask>? OnParticipantsUpdated;

    private readonly Dictionary<Type, Func<object, string?, ValueTask>> _eventDictionary;

    public CallingServerEventDispatcher()
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
            [typeof(AddParticipantSucceeded)] = async (@event, contextId) =>
            {
                if (OnAddParticipantSucceeded is null) return;
                await OnAddParticipantSucceeded.Invoke((AddParticipantSucceeded)@event, contextId);
            },
            [typeof(AddParticipantFailed)] = async (@event, contextId) =>
            {
                if (OnAddParticipantFailed is null) return;
                await OnAddParticipantFailed.Invoke((AddParticipantFailed)@event, contextId);
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
            [typeof(RemoveParticipantSucceeded)] = async (@event, contextId) =>
            {
                if (OnRemoveParticipantSucceeded is null) return;
                await OnRemoveParticipantSucceeded.Invoke((RemoveParticipantSucceeded)@event, contextId);
            },
            [typeof(RemoveParticipantFailed)] = async (@event, contextId) =>
            {
                if (OnRemoveParticipantFailed is null) return;
                await OnRemoveParticipantFailed.Invoke((RemoveParticipantFailed)@event, contextId);
            },
            [typeof(ParticipantsUpdated)] = async (@event, contextId) =>
            {
                if (OnParticipantsUpdated is null) return;
                await OnParticipantsUpdated.Invoke((ParticipantsUpdated)@event, contextId);
            }
        };
    }

    public void Dispatch(object @event, Type eventType, string contextId = default!)
    {
        _eventDictionary[eventType](@event, contextId);
    }
}