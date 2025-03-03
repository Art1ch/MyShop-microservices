using MediatR;
using StoreService.Core.Entities;

namespace StoreService.Application.Commands.Basket.AddProduct
{
    public class AddProductToBasketCommand : IRequest
    {
        public Guid BasketId { get; set; }
        public Guid ProductId { get; set; }
        public int Amount { get; set; }
    }
}
