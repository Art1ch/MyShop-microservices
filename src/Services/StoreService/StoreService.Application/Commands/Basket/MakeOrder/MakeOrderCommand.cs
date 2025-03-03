using MediatR;

namespace StoreService.Application.Commands.Basket.MakeOrder
{
    public class MakeOrderCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
    }
}
