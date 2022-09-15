// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

namespace JasonShave.Azure.Communication.Service.JobRouter.Sdk.Contracts.V2021_10_20_preview.Models;

public sealed class ChannelConfiguration
{
    public string ChannelId { get; set; }

    public int CapacityCostPerJob { get; set; }
}