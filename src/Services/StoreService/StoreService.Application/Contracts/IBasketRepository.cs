using OneOf;
using Shared.Results;
using StoreService.Core.Entities;

namespace StoreService.Application.Contracts
{
    public interface IBasketRepository
    {
        Task<OneOf<Success<Guid>, Failed>> CreateBasketAsync(BasketEntity basket, CancellationToken cancellationToken=default);
        Task<OneOf<Success<List<BasketEntity>>, Failed>> GetAllAsync(CancellationToken cancellationToken=default);
        Task<OneOf<Success, Failed>> DeleteBasketAsync(Guid id, CancellationToken cancellationToken=default);
        Task<OneOf<Success<Guid>, Failed>> UpdateBasketAsync(BasketEntity basket, CancellationToken cancellationToken=default);
        Task<OneOf<Success<BasketEntity>,Failed>> GetBasketByIdAsync(Guid id, CancellationToken cancellationToken=default);
        Task<OneOf<Success,Failed>> AddProductToBasket(Guid basketId, Guid productId, int amount, CancellationToken cancellationToken=default);
        Task<OneOf<Success, Failed>> DeleteProductFromBasketAsync(Guid basketId, Guid productId, CancellationToken cancellationToken=default);
        Task<OneOf<Success, Failed>> UpdateProductInBasketAsync(Guid basketId, Guid productId, int amount, CancellationToken cancellation=default);
    }
}
