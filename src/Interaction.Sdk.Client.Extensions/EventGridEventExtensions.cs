using Azure.Messaging.EventGrid;
using JasonShave.Azure.Communication.Service.Interaction.Sdk.Contracts.V2022_11_1.Events;

namespace JasonShave.Azure.Communication.Service.Interaction.Sdk.Client.Extensions;

public static class EventGridEventExtensions
{
    private const string IncomingCallEvent = "Microsoft.Communication.IncomingCall";

    public static IncomingCall? ToIncomingCallEvent(this EventGridEvent? @event)
    {
        if (@event is null) return null;

        return @event.EventType == IncomingCallEvent
            ? @event.Data.ToObjectFromJson<IncomingCall>()
            : null;
    }
}