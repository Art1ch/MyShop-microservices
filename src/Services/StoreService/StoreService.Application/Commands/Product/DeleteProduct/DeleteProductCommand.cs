using MediatR;

namespace StoreService.Application.Commands.Product.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
