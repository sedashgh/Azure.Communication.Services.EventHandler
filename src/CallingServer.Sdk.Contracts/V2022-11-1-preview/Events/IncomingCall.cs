using System.Text.Json.Serialization;

namespace JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts.V2022_11_1_preview.Events;

[Serializable]
public class IncomingCall
{
    [JsonPropertyName("to")]
    public Identifier To { get; set; }

    [JsonPropertyName("from")]
    public Identifier From { get; set; }

    [JsonPropertyName("hasIncomingVideo")]
    public bool HasIncomingVideo { get; set; }

    [JsonPropertyName("callerDisplayName")]
    public string CallerDisplayName { get; set; }

    [JsonPropertyName("incomingCallContext")]
    public string IncomingCallContext { get; set; }

    [JsonPropertyName("correlationId")]
    public string CorrelationId { get; set; }

}

[Serializable]
public class Identifier
{
    [JsonPropertyName("rawId")]
    public string RawId { get; set; }

    [JsonPropertyName("phoneNumber")]
    public PhoneNumber PhoneNumber { get; set; }
}

[Serializable]
public class PhoneNumber
{
    [JsonPropertyName("value")]
    public string Value { get; set; }
}