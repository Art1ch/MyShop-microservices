using OneOf;
using Shared.Results;
using StoreService.Application.Repsonses.CommandResponses.Product;
using StoreService.Application.Repsonses.QueriesResponses.Product;
using StoreService.Core.Entities;

namespace StoreService.Application.Contracts
{
    public interface IProductRepository
    {
        Task<OneOf<Success<CreateProductResponse>, Failed>> CreateProductAsync(ProductEntity product, CancellationToken cancellationToken=default);
        Task<OneOf<Success<GetAllProductsResponse>, Failed>> GetAllAsync(CancellationToken cancellationToken=default);
        Task<OneOf<Success<DeleteProductResponse>, Failed>> DeleteProductAsync(Guid id, CancellationToken cancellationToken= default);
        Task<OneOf<Success<UpdateProductResponse>, Failed>> UpdateProductAsync(ProductEntity product, CancellationToken cancellationToken=default);
        Task<OneOf<Success<GetProductByIdResponse>, Failed>> GetProductByIdAsync(Guid id, CancellationToken cancellationToken=default);
    }
}
