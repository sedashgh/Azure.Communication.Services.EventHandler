# Calling Server Client Extensions Library

This .NET library provides a set of convenience layer services and extensions to the `Azure.Communication.Service.CallingServer` library currently in Public Preview.

## Common event handling challenges

A common task developers must undertake with an event-driven platform is to deal with a common event payload which wraps a variation of models often denoted with a type identifier. Consider the following event, `CallConnected` which is 'wrapped' in an `Azure.Messaging.CloudEvent` type:

### Azure.Messaging.CloudEvent

```json
{
    "id": "7dec6eed-129c-43f3-a2bf-134ac1978168",
    "source": "calling/callConnections/441f1200-fd54-422e-9566-a867d187dca7/callState",
    "type": "Microsoft.Communication.CallConnected",
    "data": {
        "callConnectionId": "441f1200-fd54-422e-9566-a867d187dca7",
        "callConnectionState": "connected"
    },
    "time": "2022-06-24T15:12:41.5556858+00:00",
    "specversion": "1.0",
    "datacontenttype": "application/json",
    "subject": "calling/callConnections/441f1200-fd54-422e-9566-a867d187dca7/callState"
}
```

Since this event is triggered by a web hook callback, the API controller consuming this `CloudEvent` type must extract the body by reading the `HttpRequest` object, typically asynchronously, then deserialize it to it's proper type as follows:

```csharp
string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
CloudEvent[] cloudEvent = JsonSerializer.Deserialize<CloudEvent[]>(requestBody);
```

You then need to invoke conditional logic with "magic strings" against the `type` property to understand what payload exists in the `data` object, then deserialize that into something useful as follows:

```csharp
if (cloudEvent[0].Type == "Microsoft.Communication.CallConnected")
{
    CallConnected callConnectedEvent = JsonSerializer.Deserialize<CallConnected>(cloudEvent[0].Data);
    // now you can invoke your action based on this event
}
```

This needs to be done by every customer for every possible event type which turns the focus of the developer away from the business problem they're solving and concerns them with the non-functional challenges of working with such a platform.

## Setup & configuration

1. Clone this repository and add it as a reference to your .NET project.
2. Set your Azure Communication Service `ConnectionString` property in your [.NET User Secrets store](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows), `appsettings.json`, or anywhere your `IConfiguration` provider can look for the `QueueClientSettings`. For example:

    ```json
    {
        "CallingServerClientSettings" : {
            "ConnectionString": "[your_connection_string]"
        }
    }
    ```

## Setting up dependency injection

1. Add the following to your .NET 6 or higher `Program.cs` file:

    ```csharp
    var builder = WebApplication.CreateBuilder(args);

    // add this line to allow DI for the CallingServerClient
    builder.Services.AddAzureCommunicationServicesCallingServerClient(options => 
        builder.Configuration.Bind(nameof(CallingServerClientSettings), options));
    
    var app = builder.Build();

    // add this line to populate the Event Catalog
    app.UseAzureCallingServerEventDispatcher();

    app.Run();
    ```

## Sending CloudEvents

Using .NET constructor injection, add the `ICallingServerEventSender` to push `Azure.Messaging.CloudEvent` types into the service where their types are automatically determined/deserialized, and the correct event handler is invoked.

```csharp
public class CloudEventHandler : IEventHandler<CloudEvent>
{
    private readonly ICallingServerEventSender _eventSender;
    
    public CloudEventHandler(ICallingServerEventSender eventSender) => _eventSender = eventSender;

    public async Task Handle(CloudEvent cloudEvent, CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            _eventSender.Send(cloudEvent.Data, cloudEvent.Type);
        }
    }
}
```

## Subscribing to CallingServer events

As mentioned above, the `CloudEvent` is pushed and the corresponding C# event is invoked and subscribed to as follows:

```csharp
public class CallingServerEventWorkerService : IHostedService
{
    public CallingServerEventWorkerService(ICallingServerEventSubscriber eventSubscriber)
    {        
        // subscribe to events
        eventSubscriber.OnCallConnectionStateChanged += HandleOnCallConnectionStateChanged;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            Task.Delay(1000, cancellationToken);
        }

        return Task.CompletedTask;
    }

    private ValueTask HandleOnCallConnectionStateChanged(CallConnectionStateChanged args)
    {
        _logger.LogInformation($"Call connection ID: {args.CallConnectionId}");
        return ValueTask.CompletedTask;
    }
}
```

## Injecting the CallingServerClient using DI

Following the guidance in the configuration section above, the `CallingServerClient` is registered as a singleton in .NET's dependency injection container. Simply use it with constructor injection as follows:

```csharp
public class Worker
{
    private readonly CallingServerClient _client;

    public Worker(CallingServerClient client) => _client = client;

    public async Task DoWork()
    {
        await _client.CreateCallAsync();
    }
}
```

## License

This project is licensed under the MIT License - see the [LICENSE.md](license.md) file for details.
