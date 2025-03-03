using EventBus.Events.User.Create;
using MassTransit;
using StoreService.Application.Contracts;
using StoreService.Core.Entities;

namespace StoreService.Infrastructure.Consumer
{
    public class UserCreatedEventConsumer : IConsumer<UserCreatedEvent>
    {
        private readonly IBasketRepository _basketRepository;
        public UserCreatedEventConsumer(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            var basket = new BasketEntity()
            {
                Id = context.Message.Id,
                Price = 0m,
                CreatedTime = DateTime.UtcNow,
                Products = new List<ProductEntity>()
            };

            await _basketRepository.CreateBasketAsync(basket);
        }
    }
}
