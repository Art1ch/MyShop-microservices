using MediatR;
using StoreService.Application.Contracts;
using StoreService.Core.Entities;

namespace StoreService.Application.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new ProductEntity()
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Amount = request.Amount,
                Price = request.Price,
            };

            return await _productRepository.UpdateProductAsync(product, cancellationToken);
        }
    }
}
