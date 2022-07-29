// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

namespace JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts;

public struct AcsCallData
{
    public string? CallConnectionId { get; }

    public string? CorrelationId { get; }

    public string? ServerCallId { get; }

    public AcsCallData(string callConnectionId, string correlationId, string serverCallId)
    {
        CallConnectionId = callConnectionId;
        CorrelationId = correlationId;
        ServerCallId = serverCallId;
    }
}