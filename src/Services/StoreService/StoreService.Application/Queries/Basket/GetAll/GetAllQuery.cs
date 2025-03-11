using MediatR;
using OneOf;
using Shared.Results;
using StoreService.Application.Repsonses.QueriesResponses.Basket;
using StoreService.Core.Entities;

namespace StoreService.Application.Queries.Basket.GetAll
{
    public class GetAllQuery : IRequest<OneOf<Success<GetAllBasketsResponse>, Failed>>
    {
        
    }
}
