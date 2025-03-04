using MediatR;
using OneOf;
using Shared.Results;

namespace StoreService.Application.Commands.Basket.AddProduct
{
    public class AddProductToBasketCommand : IRequest<OneOf<Success, Failed>>
    {
        public Guid BasketId { get; set; }
        public Guid ProductId { get; set; }
        public int Amount { get; set; }
    }
}
