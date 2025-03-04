using MediatR;
using OneOf;
using Shared.Results;
using StoreService.Application.Contracts;
using StoreService.Core.Entities;

namespace StoreService.Application.Queries.Product.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, OneOf<Success<List<ProductEntity>>, Failed>>
    {
        private IProductRepository _productRepository;

        public GetAllQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<OneOf<Success<List<ProductEntity>>, Failed>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetAllAsync();
        }
    }
}
