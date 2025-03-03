using EventBus.EventBus.Interface;
using EventBus.Events.Order.Create;
using MediatR;

namespace StoreService.Application.Commands.Basket.MakeOrder
{
    public class MakeOrderCommandHandler : IRequestHandler<MakeOrderCommand, Guid>
    {
        private readonly IEventBus _eventBus;
        public MakeOrderCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        public async Task<Guid> Handle(MakeOrderCommand request, CancellationToken cancellationToken)
        {
            await _eventBus.PublishAsync(new MakeOrderEvent(request.Id, request.Price), cancellationToken);
            return request.Id;
        }
    }
}
