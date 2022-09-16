# Azure Communication Services Event Handler Library

[![.NET](https://github.com/jasonshave/Azure.Communication.Service/actions/workflows/dotnet.yml/badge.svg)](https://github.com/jasonshave/Azure.Communication.Service/actions/workflows/dotnet.yml)

This repository contains libraries which act as a set of convenience layer services to the `Azure.Communication.Service.CallAutomation` and `Azure.Communication.Service.JobRouter` libraries currently in preview.

## Problem statement

A common task developers must undertake with an event-driven platform is to deal with a common event payload which wraps a variation of models often denoted with a type identifier. Consider the following event, `CallConnected` which is 'wrapped' in an `Azure.Messaging.CloudEvent` type:

### Azure.Messaging.CloudEvent

```json
{
    "id": "7dec6eed-129c-43f3-a2bf-134ac1978168",
    "source": "calling/callConnections/441f1200-fd54-422e-9566-a867d187dca7",
    "type": "Microsoft.Communication.CallConnected",
    "data": {
        "callConnectionId": "441f1200-fd54-422e-9566-a867d187dca7",
        "serverCallId": "e42f9a50-c36d-493a-9cc0-2fc1cdc7b708",
        "correlationId": "7a659f41-bd0f-4bae-8ac0-6af79283e1bc"
    },
    "time": "2022-06-24T15:12:41.5556858+00:00",
    "specversion": "1.0",
    "datacontenttype": "application/json",
    "subject": "calling/callConnections/441f1200-fd54-422e-9566-a867d187dca7"
}
```

Since this event is triggered by a web hook callback, the API controller consuming this `CloudEvent` type must extract the body by reading the `HttpRequest` object, typically asynchronously, then deserialize it to it's proper type. The `CloudEvent` open specification provides a `type` property to aid developers in understanding the `data` payload type. The following example illustrates the cumbersome nature of handling different data payloads in a single `CloudEvent` envelope:

```csharp
// get body from HttpRequest
string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

// deserialize into an array of CloudEvent types
CloudEvent[] cloudEvents = JsonSerializer.Deserialize<CloudEvent[]>(requestBody);

foreach(var cloudEvent in cloudEvents)
{
    var @event = CallAutomationEventParser.Parse(cloudEvent);
    // conditional logic for every possible event type
    if (@event is CallConnected callConnected)
    {        
        // now you can invoke your action based on this event being cast to the correct type
    }
}
```

This conditional logic handling needs to be done by every customer for every possible event type which turns the focus of the developer away from their business problem and concerns them with the non-functional challenges.

## Call Automation and/or Job Router Configuration

The following NuGet packages are available depending on if you want to handle Call Automation events, JobRouter events, or both.

| Package | Latest | Details
|--|--|--|
| EventHandler.CallAutomation | [![Nuget](https://img.shields.io/nuget/v/JasonShave.Azure.Communication.Service.EventHandler.CallAutomation.svg?style=flat)](https://www.nuget.org/packages/JasonShave.Azure.Communication.Service.EventHandler.CallAutomation/)   | Used with ACS Call Automation SDK |
| EventHandler.JobRouter | [![Nuget](https://img.shields.io/nuget/v/JasonShave.Azure.Communication.Service.EventHandler.JobRouter.svg?style=flat)](https://www.nuget.org/packages/JasonShave.Azure.Communication.Service.EventHandler.JobRouter/) | Used with ACS Job Router SDK |

For a typical .NET 6 web application, the following configuration can be made to wire up the publishers, event catalog, dispatcher, and allow you to subscribe to events from either platform.

1. Add the following to your .NET 6 or higher `Program.cs` file:

    ```csharp
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddEventHandlerServices() //<--adds common event handling services
        .AddCallAutomationEventHandling() //<--adds support for Call Automation SDK events
        .AddJobRouterEventHandling(); //<--adds support for Job Router SDK events
    
    var app = builder.Build();
    app.Run();
    ```

## Publishing CloudEvents and EventGridEvents

Leveraging .NET's dependency injection framework, add the `IEventPublisher<Calling>` or `IEventPublisher<Router>` to push `Azure.Messaging.CloudEvent` and `Azure.Messaging.EventGrid` messages into the services where their types are automatically cast, deserialized, and the correct event handler is invoked.

```csharp
// .NET 6 'minimal API' to handle JobRouter Event Grid HTTP web hook subscription
app.MapPost("/api/jobRouter", (
    [FromBody] EventGridEvent[] eventGridEvents,
    [FromServices] IEventPublisher<Router> publisher) =>
{
    foreach (var eventGridEvent in eventGridEvents)
    {
        publisher.Publish(eventGridEvent);
    }

    return Results.Ok();
}).Produces(StatusCodes.Status200OK);

// .NET 6 'minimal API' to handle Call Automation mid-call web hook callbacks
app.MapPost("/api/calls/{contextId}", (
    [FromBody] CloudEvent[] cloudEvent,
    [FromRoute] string contextId,
    [FromServices] IEventPublisher<Calling> publisher) =>
{
    foreach (var @event in cloudEvent)
    {
        publisher.Publish(@event.Data, contextId);
    }

    return Results.Ok();
}).Produces(StatusCodes.Status200OK);
```

## Subscribing and handling Call Automation and Job Router events

As mentioned above, the `CloudEvent` or `EventGridEvent` is pushed and the corresponding C# event is invoked and subscribed to. For the Call Automation SDK, incoming calls are delivered using Event Grid and mid-call events are delivered through web hook callbacks. Job Router uses Event Grid to deliver all events. The example below shows a .NET background service wiring up the event handler as follows:

```csharp
public class MyService : BackgroundService
{
    public MyService(
        ICallAutomationEventSubscriber callAutomationEventSubscriber,
        IJobRouterEventSubscriber jobRouterSubscriber)
    {
        // subscribe to Call Automation and Job Router events
        callAutomationEventSubscriber.OnCallConnected += HandleCallConnected;
        jobRouterSubscriber.OnJobQueued += HandleJobQueued;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            Task.Delay(1000, cancellationToken);
        }
    }

    private async Task HandleCallConnected(CallConnected callConnected, string contextId) =>
        _logger.LogInformation($"Call connection ID: {callConnected.CallConnectionId} | Context: {contextId}");

    private Task HandleJobQueued(RouterJobQueued jobQueued, string contextId) =>
        _logger.LogInformation($"Job {jobQueued.JobId} in queue {jobQueued.QueueId}")
}
```

## License

This project is licensed under the MIT License - see the [LICENSE.md](license.md) file for details.
