using Azure.Messaging.EventGrid;
using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Events;
using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Models;

namespace JasonShave.Azure.Communication.Service.CallingServer.Extensions.Extensions;

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