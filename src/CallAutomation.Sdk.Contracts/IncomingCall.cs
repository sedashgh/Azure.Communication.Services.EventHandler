// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using JasonShave.Azure.Communication.Service.EventHandler.Sdk.Common;

namespace JasonShave.Azure.Communication.Service.CallAutomation.Sdk.Contracts;

[Serializable]
public class IncomingCall
{
    public CommunicationIdentifier To { get; set; } = default!;

    public CommunicationIdentifier From { get; set; } = default!;

    public string CallerDisplayName { get; set; } = default!;

    public string IncomingCallContext { get; set; } = default!;

    public string CorrelationId { get; set; } = default!;
}