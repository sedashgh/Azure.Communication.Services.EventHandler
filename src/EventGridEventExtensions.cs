using Azure.Messaging.EventGrid;
using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Version_2022_11_1.Events;

namespace JasonShave.Azure.Communication.Service.CallingServer.Extensions;

public static class EventGridEventExtensions
{
    private const string IncomingCallEvent = "Microsoft.Communication.IncomingCall";

    public static IncomingCallEvent? ToIncomingCallEvent(this EventGridEvent? @event)
    {
        if (@event is null) return null;

        return @event.EventType == IncomingCallEvent
            ? @event.Data.ToObjectFromJson<IncomingCallEvent>()
            : null;
    }
}