using MediatR;
using StoreService.Application.Contracts;
using StoreService.Core.Entities;

namespace StoreService.Application.Queries.Product.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductEntity>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;  
        }

        public async Task<ProductEntity?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetProductByIdAsync(request.Id);
        }
    }
}
