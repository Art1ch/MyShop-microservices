using MediatR;
using OneOf;
using Shared.Results;
using StoreService.Core.Entities;

namespace StoreService.Application.Queries.Product.GetAll
{
    public class GetAllQuery : IRequest<OneOf<Success<List<ProductEntity>>, Failed>>
    {

    }
}
