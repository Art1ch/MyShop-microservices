using MediatR;
using StoreService.Core.Entities;

namespace StoreService.Application.Queries.Basket.GetAll
{
    public class GetAllQuery : IRequest<List<BasketEntity>>
    {
        
    }
}
