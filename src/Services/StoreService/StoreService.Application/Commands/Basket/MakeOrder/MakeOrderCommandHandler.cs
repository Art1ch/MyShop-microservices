using EventBus.EventBus.Interface;
using EventBus.Events.Order.Create;
using MediatR;
using OneOf;
using Shared.Results;
using StoreService.Application.Repsonses.CommandResponses.Basket;

namespace StoreService.Application.Commands.Basket.MakeOrder
{
    public class MakeOrderCommandHandler : IRequestHandler<MakeOrderCommand, OneOf<Success<MakeOrderResponse>, Failed>>
    {
        private readonly IEventBus _eventBus;
        public MakeOrderCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        public async Task<OneOf<Success<MakeOrderResponse>, Failed>> Handle(MakeOrderCommand request, CancellationToken cancellationToken)
        {
            await _eventBus.PublishAsync(new MakeOrderEvent(request.Id, request.Price), cancellationToken);
            return new Success<MakeOrderResponse>(new MakeOrderResponse());
        }
    }
}
