using AutoFixture;
using FluentAssertions;
using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Events;
using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Version_2022_11_1;
using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Version_2022_11_1.Dispatcher;

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

        var subject = new EventDispatcher<V2022_11_1>();

        subject.OnCallConnected += (sender, args) =>
        {
            sender.Should().NotBeNull();
            args.Should().NotBeNull();
            args.Event.Should().BeOfType<CallConnectedEvent>();
        };

        subject.OnCallDisconnected += (sender, args) =>
        {
            sender.Should().NotBeNull();
            args.Should().NotBeNull();
            args.Event.Should().BeOfType<CallDisconnectedEvent>();
        };

        subject.OnCallConnectionStateChanged += (sender, args) =>
        {
            sender.Should().NotBeNull();
            args.Should().NotBeNull();
            args.Event.Should().BeOfType<CallConnectionStateChanged>();
        };

        // act
        subject.Dispatch(callConnectedEvent);
        subject.Dispatch(callDisconnectedEvent);
        subject.Dispatch(callConnectionStateChangedEvent);
    }
}