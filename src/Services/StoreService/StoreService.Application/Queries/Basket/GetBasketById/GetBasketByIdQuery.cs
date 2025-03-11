using MediatR;
using OneOf;
using Shared.Results;
using StoreService.Application.Repsonses.QueriesResponses.Basket;
using StoreService.Core.Entities;

namespace StoreService.Application.Queries.Basket.GetBasketById
{
    public class GetBasketByIdQuery : IRequest<OneOf<Success<GetBasketByIdResponse>, Failed>>
    {
        public Guid Id { get; set; }

        public GetBasketByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
