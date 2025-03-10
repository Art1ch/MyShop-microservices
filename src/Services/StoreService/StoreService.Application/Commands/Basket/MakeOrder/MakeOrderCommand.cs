using MediatR;
using OneOf;
using Shared.Results;
using StoreService.Application.Repsonses.CommandResponses.Basket;

namespace StoreService.Application.Commands.Basket.MakeOrder
{
    public class MakeOrderCommand : IRequest<OneOf<Success<MakeOrderResponse>, Failed>>
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
    }
}
