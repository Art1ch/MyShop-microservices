using MediatR;
using OneOf;
using Shared.Results;

namespace StoreService.Application.Commands.Product.DeleteProduct
{
    public class DeleteProductCommand : IRequest<OneOf<Success, Failed>>
    {
        public Guid Id { get; set; }
    }
}
