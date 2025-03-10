using MediatR;
using OneOf;
using Shared.Results;
using StoreService.Application.Repsonses.CommandResponses.Basket;

namespace StoreService.Application.Commands.Basket.AddProduct
{
    public class AddProductToBasketCommand : IRequest<OneOf<Success<AddProductToBasketResponse>, Failed>>
    {
        public Guid BasketId { get; set; }
        public Guid ProductId { get; set; }
        public int Amount { get; set; }
    }
}
