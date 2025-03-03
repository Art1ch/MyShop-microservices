using StoreService.Core.Entities;

namespace StoreService.Application.Contracts
{
    public interface IBasketRepository
    {
        Task<Guid> CreateBasketAsync(BasketEntity basket, CancellationToken cancellationToken=default);
        Task<List<BasketEntity>> GetAllAsync(CancellationToken cancellationToken=default);
        Task DeleteBasketAsync(Guid id, CancellationToken cancellationToken=default);
        Task<Guid> UpdateBasketAsync(BasketEntity basket, CancellationToken cancellationToken=default);
        Task<BasketEntity?> GetBasketByIdAsync(Guid id, CancellationToken cancellationToken=default);
        Task AddProductToBasket(Guid basketId, Guid productId, int amount, CancellationToken cancellationToken=default);
        Task DeleteProductFromBasketAsync(Guid basketId, Guid productId, CancellationToken cancellationToken=default);
        Task UpdateProductInBasketAsync(Guid basketId, Guid productId, int amount, CancellationToken cancellation=default);
    }
}
