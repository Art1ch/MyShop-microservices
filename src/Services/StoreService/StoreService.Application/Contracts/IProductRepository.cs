using StoreService.Core.Entities;

namespace StoreService.Application.Contracts
{
    public interface IProductRepository
    {
        Task<Guid> CreateProductAsync(ProductEntity product, CancellationToken cancellationToken=default);
        Task<List<ProductEntity>> GetAllAsync(CancellationToken cancellationToken=default);
        Task DeleteProductAsync(Guid id, CancellationToken cancellationToken= default);
        Task<Guid> UpdateProductAsync(ProductEntity product, CancellationToken cancellationToken=default);
        Task<ProductEntity?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken=default);
    }
}
