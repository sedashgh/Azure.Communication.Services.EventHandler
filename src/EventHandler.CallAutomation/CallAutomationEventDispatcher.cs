// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using Azure.Communication.CallAutomation;
using CallAutomation.Contracts;

namespace JasonShave.Azure.Communication.Service.EventHandler.CallAutomation;

internal sealed class CallAutomationEventDispatcher : IEventDispatcher<Calling>, ICallAutomationEventSubscriber
{
    public event Func<IncomingCall, string?, ValueTask>? OnIncomingCall;
    public event Func<CallConnected, string?, ValueTask>? OnCallConnected;
    public event Func<CallDisconnected, string?, ValueTask>? OnCallDisconnected;
    public event Func<AddParticipantSucceeded, string?, ValueTask>? OnAddParticipantsSucceeded;
    public event Func<AddParticipantFailed, string?, ValueTask>? OnAddParticipantsFailed;
    public event Func<CallTransferAccepted, string?, ValueTask>? OnCallTransferAccepted;
    public event Func<CallTransferFailed, string?, ValueTask>? OnCallTransferFailed;
    public event Func<ParticipantsUpdated, string?, ValueTask>? OnParticipantsUpdated;
    public event Func<PlayCompleted, string?, ValueTask>? OnPlayCompleted;
    public event Func<PlayFailed, string?, ValueTask>? OnPlayFailed;
    public event Func<RecognizeCompleted, string?, ValueTask> OnRecognizeCompleted;
    public event Func<RecognizeFailed, string?, ValueTask> OnRecognizeFailed;
    public event Func<RecordingStateChanged, string?, ValueTask>? OnRecordingStateChanged;
    public event Func<PlayCanceled, string?, ValueTask>? OnPlayCanceled;
    public event Func<ContinuousDtmfRecognitionToneReceived, string?, ValueTask> OnContinuousDtmfRecognitionToneReceived;

    private readonly Dictionary<Type, Func<object, string?, ValueTask>> _eventDictionary;

    public CallAutomationEventDispatcher()
    {
        _eventDictionary = new Dictionary<Type, Func<object, string?, ValueTask>>
        {
            [typeof(IncomingCall)] = async (@event, contextId) =>
            {
                if (OnIncomingCall is null) return;
                await OnIncomingCall.Invoke((IncomingCall)@event, contextId).ConfigureAwait(false);
            },
            [typeof(CallConnected)] = async (@event, contextId) =>
            {
                if (OnCallConnected is null) return;
                await OnCallConnected.Invoke((CallConnected)@event, contextId).ConfigureAwait(false);
            },
            [typeof(CallDisconnected)] = async (@event, contextId) =>
            {
                if (OnCallDisconnected is null) return;
                await OnCallDisconnected.Invoke((CallDisconnected)@event, contextId).ConfigureAwait(false);
            },
            [typeof(AddParticipantSucceeded)] = async (@event, contextId) =>
            {
                if (OnAddParticipantsSucceeded is null) return;
                await OnAddParticipantsSucceeded.Invoke((AddParticipantSucceeded)@event, contextId).ConfigureAwait(false);
            },
            [typeof(AddParticipantFailed)] = async (@event, contextId) =>
            {
                if (OnAddParticipantsFailed is null) return;
                await OnAddParticipantsFailed.Invoke((AddParticipantFailed)@event, contextId).ConfigureAwait(false);
            },
            [typeof(CallTransferAccepted)] = async (@event, contextId) =>
            {
                if (OnCallTransferAccepted is null) return;
                await OnCallTransferAccepted.Invoke((CallTransferAccepted)@event, contextId).ConfigureAwait(false);
            },
            [typeof(CallTransferFailed)] = async (@event, contextId) =>
            {
                if (OnCallTransferFailed is null) return;
                await OnCallTransferFailed.Invoke((CallTransferFailed)@event, contextId).ConfigureAwait(false);
            },
            [typeof(ParticipantsUpdated)] = async (@event, contextId) =>
            {
                if (OnParticipantsUpdated is null) return;
                await OnParticipantsUpdated.Invoke((ParticipantsUpdated)@event, contextId).ConfigureAwait(false);
            },
            [typeof(PlayCompleted)] = async (@event, contextId) =>
            {
                if (OnPlayCompleted is null) return;
                await OnPlayCompleted.Invoke((PlayCompleted)@event, contextId).ConfigureAwait(false);
            },
            [typeof(PlayFailed)] = async (@event, contextId) =>
            {
                if (OnPlayFailed is null) return;
                await OnPlayFailed.Invoke((PlayFailed)@event, contextId).ConfigureAwait(false);
            },
            [typeof(RecognizeCompleted)] = async (@event, contextId) =>
            {
                if (OnRecognizeCompleted is null) return;
                await OnRecognizeCompleted.Invoke((RecognizeCompleted)@event, contextId).ConfigureAwait(false);
            },
            [typeof(RecognizeFailed)] = async (@event, contextId) =>
            {
                if (OnRecognizeFailed is null) return;
                await OnRecognizeFailed.Invoke((RecognizeFailed)@event, contextId).ConfigureAwait(false);
            },
            [typeof(RecordingStateChanged)] = async (@event, contextId) =>
            {
                if (OnRecordingStateChanged is null) return;
                await OnRecordingStateChanged.Invoke((RecordingStateChanged)@event, contextId).ConfigureAwait(false);
            },
            [typeof(PlayCanceled)] = async (@event, contextId) =>
            {
                if (OnPlayCanceled is null) return;
                await OnPlayCanceled.Invoke((PlayCanceled)@event, contextId).ConfigureAwait(false);
            },
            [typeof(ContinuousDtmfRecognitionToneReceived)] = async (@event, contextId) =>
            {
                if (OnContinuousDtmfRecognitionToneReceived is null) return;
                await OnContinuousDtmfRecognitionToneReceived.Invoke((ContinuousDtmfRecognitionToneReceived)@event, contextId).ConfigureAwait(false);
            },
        };
    }

    public void Dispatch(object @event, string? contextId = default)
    {
        _eventDictionary[@event.GetType()](@event, contextId);
    }
}