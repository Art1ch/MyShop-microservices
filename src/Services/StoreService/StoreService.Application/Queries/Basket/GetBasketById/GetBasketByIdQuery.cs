using MediatR;
using OneOf;
using Shared.Results;
using StoreService.Core.Entities;

namespace StoreService.Application.Queries.Basket.GetBasketById
{
    public class GetBasketByIdQuery : IRequest<OneOf<Success<BasketEntity>, Failed>>
    {
        public Guid Id { get; set; }

        public GetBasketByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
