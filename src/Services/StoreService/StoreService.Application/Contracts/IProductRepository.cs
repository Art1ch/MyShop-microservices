using OneOf;
using Shared.Results;
using StoreService.Core.Entities;

namespace StoreService.Application.Contracts
{
    public interface IProductRepository
    {
        Task<OneOf<Success<Guid>, Failed>> CreateProductAsync(ProductEntity product, CancellationToken cancellationToken=default);
        Task<OneOf<Success<List<ProductEntity>>, Failed>> GetAllAsync(CancellationToken cancellationToken=default);
        Task<OneOf<Success, Failed>> DeleteProductAsync(Guid id, CancellationToken cancellationToken= default);
        Task<OneOf<Success<Guid>, Failed>> UpdateProductAsync(ProductEntity product, CancellationToken cancellationToken=default);
        Task<OneOf<Success<ProductEntity>, Failed>> GetProductByIdAsync(Guid id, CancellationToken cancellationToken=default);
    }
}
