using MediatR;
using StoreService.Application.Contracts;

namespace StoreService.Application.Commands.Product.DeleteProduct
{
    public class DelteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        public DelteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _productRepository.DeleteProductAsync(request.Id, cancellationToken);
        }
    }
}
