using System.Text.Json.Serialization;

namespace JasonShave.Azure.Communication.Service.JobRouter.Sdk.Contracts.V2021_10_20_preview.Models
{
    public class WorkerSelector
    {
        public string Key { get; set; }

        public LabelOperator LabelOperator { get; set; }

        //[JsonConverter(typeof(StaticValueConverter))]
        public object Value { get; set; }

        public double? TTLSeconds { get; set; }

        public WorkerSelectorState State { get; set; }

        //[JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset? ExpireTime { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum WorkerSelectorState
    {
        Active = 0,
        Expired = 1
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LabelOperator
    {
        Equal,
        NotEqual,
        LessThan,
        LessThanEqual,
        GreaterThan,
        GreaterThanEqual,
    }
}
