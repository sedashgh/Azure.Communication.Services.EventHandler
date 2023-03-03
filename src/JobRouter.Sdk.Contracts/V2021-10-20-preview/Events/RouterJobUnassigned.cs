// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasonShave.Azure.Communication.Service.JobRouter.Sdk.Contracts.V2021_10_20_preview.Events
{
    public sealed class RouterJobUnassigned
    {
        public string JobId { get; set; }

        public string AssignmentId { get; set; }

        public string WorkerId { get; set; }

        public string ChannelId { get; set; }

        public string? ChannelReference { get; set; }

        public string? QueueId { get; set; }

        public Dictionary<string, object> Labels { get; set; }

        public Dictionary<string, object> Tags { get; set; }
    }
}
