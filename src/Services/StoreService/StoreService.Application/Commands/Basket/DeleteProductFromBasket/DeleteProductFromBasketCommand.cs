using MediatR;
using OneOf;
using Shared.Results;
using StoreService.Core.Entities;

namespace StoreService.Application.Commands.Basket.DeleteProductFromBasket
{
    public class DeleteProductFromBasketCommand : IRequest<OneOf<Success, Failed>>
    {
        public Guid BasketId { get; set; }
        public Guid ProductId { get; set; }
    }
}
