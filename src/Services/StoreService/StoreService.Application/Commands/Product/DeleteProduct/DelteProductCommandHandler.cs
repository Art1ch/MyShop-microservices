using MediatR;
using OneOf;
using Shared.Results;
using StoreService.Application.Contracts;
using StoreService.Application.Repsonses.CommandResponses.Product;

namespace StoreService.Application.Commands.Product.DeleteProduct
{
    public class DelteProductCommandHandler : IRequestHandler<DeleteProductCommand, OneOf<Success<DeleteProductResponse>, Failed>>
    {
        private readonly IProductRepository _productRepository;
        public DelteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<OneOf<Success<DeleteProductResponse>, Failed>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return await _productRepository.DeleteProductAsync(request.Id, cancellationToken);
        }
    }
}
