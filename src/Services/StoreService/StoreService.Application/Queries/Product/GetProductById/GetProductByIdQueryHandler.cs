using MediatR;
using OneOf;
using Shared.Results;
using StoreService.Application.Contracts;
using StoreService.Core.Entities;

namespace StoreService.Application.Queries.Product.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, OneOf<Success<ProductEntity>, Failed>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;  
        }

        public async Task<OneOf<Success<ProductEntity>, Failed>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetProductByIdAsync(request.Id);
        }
    }
}
