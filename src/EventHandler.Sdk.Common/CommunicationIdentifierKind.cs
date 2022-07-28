// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using System.Runtime.Serialization;

namespace JasonShave.Azure.Communication.Service.EventHandler.Sdk.Common;

public enum CommunicationIdentifierKind
{
    [EnumMember(Value= "unknown")]
    Unknown,
    [EnumMember(Value= "communicationUser")]
    CommunicationUser,
    [EnumMember(Value = "phoneNumber")]
    PhoneNumber,
    [EnumMember(Value = "microsoftTeamsUser")]
    MicrosoftTeamsUser,
}