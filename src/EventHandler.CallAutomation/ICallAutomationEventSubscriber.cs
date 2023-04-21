// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using Azure.Communication.CallAutomation;
using CallAutomation.Contracts;

namespace JasonShave.Azure.Communication.Service.EventHandler.CallAutomation;

public interface ICallAutomationEventSubscriber
{
    /// <summary>
    /// Provides an <see cref="IncomingCall"/> event and <see cref="string"/> for the context ID.
    /// </summary>
    event Func<IncomingCall, string?, ValueTask>? OnIncomingCall;

    /// <summary>
    /// Provides an <see cref="CallConnected"/> event and <see cref="string"/> for the context ID.
    /// </summary>
    event Func<CallConnected, string?, ValueTask>? OnCallConnected;

    /// <summary>
    /// Provides an <see cref="CallDisconnected"/> event and <see cref="string"/> for the context ID.
    /// </summary>
    event Func<CallDisconnected, string?, ValueTask>? OnCallDisconnected;

    /// <summary>
    /// Provides an <see cref="AddParticipantSucceeded"/> event and <see cref="string"/> for the context ID.
    /// </summary>
    event Func<AddParticipantSucceeded, string?, ValueTask>? OnAddParticipantsSucceeded;

    /// <summary>
    /// Provides an <see cref="AddParticipantFailed"/> event and <see cref="string"/> for the context ID.
    /// </summary>
    event Func<AddParticipantFailed, string?, ValueTask>? OnAddParticipantsFailed;

    /// <summary>
    /// Provides an <see cref="CallTransferAccepted"/> event and <see cref="string"/> for the context ID.
    /// </summary>
    event Func<CallTransferAccepted, string?, ValueTask>? OnCallTransferAccepted;

    /// <summary>
    /// Provides an <see cref="CallTransferFailed"/> event and <see cref="string"/> for the context ID.
    /// </summary>
    event Func<CallTransferFailed, string?, ValueTask>? OnCallTransferFailed;

    /// <summary>
    /// Provides an <see cref="ParticipantsUpdated"/> event and <see cref="string"/> for the context ID.
    /// </summary>
    event Func<ParticipantsUpdated, string?, ValueTask>? OnParticipantsUpdated;

    /// <summary>
    /// Provides an <see cref="PlayCompleted"/> event and <see cref="string"/> for the context ID.
    /// </summary>
    event Func<PlayCompleted, string?, ValueTask>? OnPlayCompleted;

    /// <summary>
    /// Provides an <see cref="PlayFailed"/> event and <see cref="string"/> for the context ID.
    /// </summary>
    event Func<PlayFailed, string?, ValueTask>? OnPlayFailed;

    /// <summary>
    /// Provides an <see cref="RecognizeCompleted"/> event and <see cref="string"/> for the context ID.
    /// </summary>
    event Func<RecognizeCompleted, string?, ValueTask> OnRecognizeCompleted;

    /// <summary>
    /// Provides an <see cref="RecognizeFailed"/> event and <see cref="string"/> for the context ID.
    /// </summary>
    event Func<RecognizeFailed, string?, ValueTask> OnRecognizeFailed;

    /// <summary>
    /// Provides an <see cref="RecordingStateChanged"/> event and <see cref="string"/> for the context ID.
    /// </summary>
    event Func<RecordingStateChanged, string?, ValueTask>? OnRecordingStateChanged;

    /// <summary>
    /// Provides an <see cref="PlayCanceld"/> event and <see cref="string"/> for the context ID.
    /// </summary>
    event Func<PlayCanceled, string?, ValueTask>? OnPlayCanceled;

    /// <summary>
    /// Provides an <see cref="ContinuousDtmfRecognitionToneReceived"/> event and <see cref="string"/> for the context ID.
    /// </summary>
    event Func<ContinuousDtmfRecognitionToneReceived, string?, ValueTask> OnContinuousDtmfRecognitionToneReceived;
}