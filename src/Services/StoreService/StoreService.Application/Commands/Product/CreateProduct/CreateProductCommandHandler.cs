using MediatR;
using StoreService.Application.Contracts;
using StoreService.Core.Entities;

namespace StoreService.Application.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var currentTime = DateTime.UtcNow;
            var product = new ProductEntity()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Amount = request.Amount,
                Price = request.Price,
                CreatedTime = currentTime,
                ModifiedTime = currentTime
            };

            return await _productRepository.CreateProductAsync(product, cancellationToken);
        }
    }
}
