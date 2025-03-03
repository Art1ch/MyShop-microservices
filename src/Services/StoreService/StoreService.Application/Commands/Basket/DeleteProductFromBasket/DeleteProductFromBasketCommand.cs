using MediatR;
using StoreService.Core.Entities;

namespace StoreService.Application.Commands.Basket.DeleteProductFromBasket
{
    public class DeleteProductFromBasketCommand : IRequest
    {
        public Guid BasketId { get; set; }
        public Guid ProductId { get; set; }
    }
}
