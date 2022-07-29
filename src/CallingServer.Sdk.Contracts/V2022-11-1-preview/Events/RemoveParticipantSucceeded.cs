// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

namespace JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts.V2022_11_1_preview.Events;

public class RemoveParticipantSucceeded : BaseCallingEvent
{
    public RemoveParticipantSucceeded(string callConnectionId, string? serverCallId, string correlationId)
        : base(callConnectionId, serverCallId, correlationId)
    {
    }
}