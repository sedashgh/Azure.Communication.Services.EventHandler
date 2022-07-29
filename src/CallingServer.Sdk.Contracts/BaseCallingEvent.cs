// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

namespace JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts;

public abstract class BaseCallingEvent
{
    public string? CallConnectionId { get; }

    public string? CorrelationId { get; }

    public string? ServerCallId { get; }

    protected BaseCallingEvent(string? callConnectionId, string? serverCallId, string? correlationId)
    {
        CallConnectionId = callConnectionId;
        CorrelationId = correlationId;
        ServerCallId = serverCallId;
    }
}