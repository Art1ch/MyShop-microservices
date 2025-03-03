using EventBus.EventBus.Interface;
using MassTransit;

namespace EventBus.EventBus.Implementations
{
    public class EventBusImplementation : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public EventBusImplementation(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class
        {
            _publishEndpoint.Publish(message, cancellationToken);
            return Task.CompletedTask;  
        }
    }
}
