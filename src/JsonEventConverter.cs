using System.Text.Json;
using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Interfaces;

namespace JasonShave.Azure.Communication.Service.CallingServer.Extensions;

public class JsonEventConverter : IEventConverter
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public JsonEventConverter()
    {
        _jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public object? Convert(string eventPayload, Type eventType)
    {
        var result = JsonSerializer.Deserialize(eventPayload, eventType, _jsonSerializerOptions);
        return result;
    }

    public object? Convert(BinaryData binaryPayload, Type eventType)
    {
        var result = JsonSerializer.Deserialize(binaryPayload, eventType, _jsonSerializerOptions);
        return result;
    }
}