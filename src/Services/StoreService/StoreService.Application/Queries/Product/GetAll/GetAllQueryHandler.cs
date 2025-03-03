using MediatR;
using StoreService.Application.Contracts;
using StoreService.Core.Entities;

namespace StoreService.Application.Queries.Product.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, List<ProductEntity>>
    {
        private IProductRepository _productRepository;

        public GetAllQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductEntity>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetAllAsync();
        }
    }
}
