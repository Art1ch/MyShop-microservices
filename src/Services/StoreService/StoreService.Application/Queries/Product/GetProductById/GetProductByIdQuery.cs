using MediatR;
using OneOf;
using Shared.Results;
using StoreService.Application.Repsonses.QueriesResponses.Product;
using StoreService.Core.Entities;

namespace StoreService.Application.Queries.Product.GetProductById
{
    public class GetProductByIdQuery: IRequest<OneOf<Success<GetProductByIdResponse>, Failed>>
    {
        public Guid Id { get; set; }

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
