// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts.V2022_11_1_preview.Events;

namespace JasonShave.Azure.Communication.Service.EventHandler.CallingServer;

public interface ICallingServerEventSubscriber
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
    /// Provides an <see cref="AddParticipantsSucceeded"/> event and <see cref="string"/> for the context ID.
    /// </summary>
    event Func<AddParticipantsSucceeded, string?, ValueTask>? OnAddParticipantsSucceeded;

    /// <summary>
    /// Provides an <see cref="AddParticipantsFailed"/> event and <see cref="string"/> for the context ID.
    /// </summary>
    event Func<AddParticipantsFailed, string?, ValueTask>? OnAddParticipantsFailed;

    /// <summary>
    /// Provides an <see cref="CallTransferAccepted"/> event and <see cref="string"/> for the context ID.
    /// </summary>
    event Func<CallTransferAccepted, string?, ValueTask>? OnCallTransferAccepted;

    /// <summary>
    /// Provides an <see cref="CallTransferFailed"/> event and <see cref="string"/> for the context ID.
    /// </summary>
    event Func<CallTransferFailed, string?, ValueTask>? OnCallTransferFailed;

    /// <summary>
    /// Provides an <see cref="RemoveParticipantSucceeded"/> event and <see cref="string"/> for the context ID.
    /// </summary>
    event Func<RemoveParticipantSucceeded, string?, ValueTask>? OnRemoveParticipantSucceeded;

    /// <summary>
    /// Provides an <see cref="RemoveParticipantFailed"/> event and <see cref="string"/> for the context ID.
    /// </summary>
    event Func<RemoveParticipantFailed, string?, ValueTask>? OnRemoveParticipantFailed;

    /// <summary>
    /// Provides an <see cref="ParticipantsUpdated"/> event and <see cref="string"/> for the context ID.
    /// </summary>
    event Func<ParticipantsUpdated, string?, ValueTask>? OnParticipantsUpdated;
}