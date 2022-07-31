// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using JasonShave.Azure.Communication.Service.EventHandler.Sdk.Common;

namespace JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts.V2022_11_1_preview.Events;

public class AddParticipantsSucceeded : BaseCallingEvent
{
    public string OperationContext { get; }

    public ResultInformation ResultInformation { get; }

    public IEnumerable<CommunicationIdentifier> Participants { get; }

    public AddParticipantsSucceeded(string operationContext, ResultInformation resultInformation, IEnumerable<CommunicationIdentifier> participants, string callConnectionId, string? serverCallId, string correlationId)
        : base(callConnectionId, serverCallId, correlationId)
    {
        OperationContext = operationContext;
        ResultInformation = resultInformation;
        Participants = participants;
    }
}