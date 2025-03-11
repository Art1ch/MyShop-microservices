using MediatR;
using OneOf;
using Shared.Results;
using StoreService.Application.Repsonses.CommandResponses.Product;

namespace StoreService.Application.Commands.Product.DeleteProduct
{
    public class DeleteProductCommand : IRequest<OneOf<Success<DeleteProductResponse>, Failed>>
    {
        public Guid Id { get; set; }
    }
}
