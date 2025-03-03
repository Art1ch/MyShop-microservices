using EventBus.Events.Order.Create;
using MassTransit;
using OrderService.Application.Contracts;
using OrderService.Core.Entities;
using OrderService.Core.StateEnum;

namespace OrderService.Infrastructure.Consumer
{
    public class MakeOrderEventConsumer : IConsumer<MakeOrderEvent>
    {
        private readonly IOrderRepository _orderRepository;
        public MakeOrderEventConsumer(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task Consume(ConsumeContext<MakeOrderEvent> context)
        {
            var order = new OrderEntity()
            {
                Id = context.Message.Id,
                Price = context.Message.Price,
                State = OrderStateEnum.Delivered,
                CreatedTime = DateTime.UtcNow,
            };

            await _orderRepository.CreateOrderAsync(order);
        }
    }
}
