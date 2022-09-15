// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

namespace JasonShave.Azure.Communication.Service.JobRouter.Sdk.Contracts.V2021_10_20_preview.Events
{
    [Serializable]
    public sealed class RouterWorkerDeregistered
    {
        public string WorkerId { get; init; }
    }
}
