// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

namespace JasonShave.Azure.Communication.Service.EventHandler.Sdk.Common;

public class MicrosoftTeamsUserIdentifier
{
    public string UserId { get; set; }

    public bool IsAnonymous { get; set; }

    public string Cloud { get; set; }
}