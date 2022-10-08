// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using FluentAssertions;
using JasonShave.Azure.Communication.Service.JobRouter.Sdk.Contracts.V2021_10_20_preview.Events;
using System.Text.Json;

namespace JasonShave.Azure.Communication.Service.JobRouter.Tests
{
    public class JobRouterContractTests
    {
        [Fact]
        public void RouterWorkerOfferIssued_Should_Deserialize()
        {
            // arrange
            var json =
                "{\r\n\t\t\"queueId\": \"0a8ffa0c-641e-4951-bfff-f80a2c9598ae\",\r\n\t\t\"workerId\": \"ef246530-940a-4bf3-8f1f-c2a144d94184\",\r\n\t\t\"offerId\": \"dec04568-65ac-4ef2-8abc-eb6ab050266d\",\r\n\t\t\"offerTimeUtc\": \"2022-10-07T14:33:00.4887458+00:00\",\r\n\t\t\"expiryTimeUtc\": \"2022-10-07T14:49:40.4887477+00:00\",\r\n\t\t\"jobPriority\": 1,\r\n\t\t\"jobLabels\": {},\r\n\t\t\"jobTags\": {},\r\n\t\t\"jobId\": \"1e16eedf-7184-40ea-8749-4888b09dfb29\",\r\n\t\t\"channelReference\": \"null\",\r\n\t\t\"channelId\": \"TestChannel_ACS\"\r\n\t}";

            // act
            var result = JsonSerializer.Deserialize(json, typeof(RouterWorkerOfferIssued), new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            }) as RouterWorkerOfferIssued;

            // assert
            result.Should().NotBeNull();
            result.QueueId.Should().Be("0a8ffa0c-641e-4951-bfff-f80a2c9598ae");
            result.WorkerId.Should().Be("ef246530-940a-4bf3-8f1f-c2a144d94184");
        }
    }
}