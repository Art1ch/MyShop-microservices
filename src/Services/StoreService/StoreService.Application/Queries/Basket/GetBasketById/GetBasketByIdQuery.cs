using MediatR;
using StoreService.Core.Entities;

namespace StoreService.Application.Queries.Basket.GetBasketById
{
    public class GetBasketByIdQuery : IRequest<BasketEntity>
    {
        public Guid Id { get; set; }

        public GetBasketByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
