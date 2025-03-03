using MediatR;
using StoreService.Core.Entities;

namespace StoreService.Application.Queries.Product.GetAll
{
    public class GetAllQuery : IRequest<List<ProductEntity>>
    {

    }
}
