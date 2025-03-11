using MediatR;
using OneOf;
using Shared.Results;
using StoreService.Application.Repsonses.CommandResponses.Basket;

namespace StoreService.Application.Commands.Basket.DeleteProductFromBasket
{
    public class DeleteProductFromBasketCommand : IRequest<OneOf<Success<DeleteProductFromBasketResponse>, Failed>>
    {
        public Guid BasketId { get; set; }
        public Guid ProductId { get; set; }
    }
}
