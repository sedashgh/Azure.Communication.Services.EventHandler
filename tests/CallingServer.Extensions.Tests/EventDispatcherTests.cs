using AutoFixture;
using FluentAssertions;
using JasonShave.Azure.Communication.Service.CallingServer.Contracts.V2022_11_1.Events;
using JasonShave.Azure.Communication.Service.CallingServer.EventHandler.Version_2022_11_1.Dispatcher;

namespace CallingServer.Extensions.Tests;

public class EventDispatcherTests
{
    [Fact(DisplayName = "Dispatch should invoke event handler")]
    public void Should_Invoke_Handler()
    {
        // arrange
        var fixture = new Fixture();
        var callConnectedEvent = fixture.Create<CallConnectedEvent>();
        var callDisconnectedEvent = fixture.Create<CallDisconnectedEvent>();
        var callConnectionStateChangedEvent = fixture.Create<CallConnectionStateChanged>();

        var subject = new CallingServerEventDispatcher();

        subject.OnCallConnected += async args =>
        {
            args.Should().NotBeNull();
            args.Should().BeOfType<CallConnectedEvent>();
        };

        subject.OnCallDisconnected += async args =>
        {
            args.Should().NotBeNull();
            args.Should().BeOfType<CallDisconnectedEvent>();
        };

        subject.OnCallConnectionStateChanged += async args =>
        {
            args.Should().NotBeNull();
            args.Should().BeOfType<CallConnectionStateChanged>();
        };

        // act
        subject.Dispatch(callConnectedEvent);
        subject.Dispatch(callDisconnectedEvent);
        subject.Dispatch(callConnectionStateChangedEvent);
    }
}