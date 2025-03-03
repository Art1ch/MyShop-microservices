using MediatR;
using StoreService.Core.Entities;

namespace StoreService.Application.Queries.Product.GetProductById
{
    public class GetProductByIdQuery: IRequest<ProductEntity>
    {
        public Guid Id { get; set; }

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
