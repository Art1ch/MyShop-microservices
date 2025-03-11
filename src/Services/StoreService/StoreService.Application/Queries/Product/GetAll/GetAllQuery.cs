using MediatR;
using OneOf;
using Shared.Results;
using StoreService.Application.Repsonses.QueriesResponses.Product;

namespace StoreService.Application.Queries.Product.GetAll
{
    public class GetAllQuery : IRequest<OneOf<Success<GetAllProductsResponse>, Failed>>
    {

    }
}
