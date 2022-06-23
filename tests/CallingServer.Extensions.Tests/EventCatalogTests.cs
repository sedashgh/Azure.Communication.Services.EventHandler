using FluentAssertions;
using JasonShave.Azure.Communication.Service.CallingServer.Extensions;
using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Version_2022_11_1;

namespace CallingServer.Extensions.Tests
{
    public class EventCatalogTests
    {
        [Fact(DisplayName = "Registering event type returns same type by name")]
        public void Register_Type_Returns_Same_Type()
        {
            // arrange
            var subject = new EventCatalog<V2022_11_1>();

            // act
            subject
                .Register<StartEvent>()
                .Register<StopEvent>();

            var startEvent = subject.Get(nameof(StartEvent));
            var stopEvent = subject.Get(nameof(StopEvent));

            // assert
            startEvent.Should().NotBeNull();
            startEvent.Should().BeAssignableTo<StartEvent>();

            stopEvent.Should().NotBeNull();
            stopEvent.Should().BeAssignableTo<StopEvent>();
        }
    }
}