using System.Text.Json;
using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Interfaces;

namespace JasonShave.Azure.Communication.Service.CallingServer.Extensions;

public class JsonEventConverter : IEventConverter
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public JsonEventConverter(JsonSerializerOptions jsonSerializerOptions)
    {
        _jsonSerializerOptions = jsonSerializerOptions;
    }

    public object? Convert(string eventPayload, Type eventType)
    {
        var result = JsonSerializer.Deserialize(eventPayload, eventType);
        return result;
    }
}