using OneOf;
using Shared.Results;
using StoreService.Application.Repsonses.CommandResponses.Basket;
using StoreService.Application.Repsonses.QueriesResponses.Basket;
using StoreService.Core.Entities;

namespace StoreService.Application.Contracts
{
    public interface IBasketRepository
    {
        Task<OneOf<Success<Guid>, Failed>> CreateBasketAsync(BasketEntity basket, CancellationToken cancellationToken=default);
        Task<OneOf<Success<GetAllBasketsResponse>, Failed>> GetAllAsync(CancellationToken cancellationToken=default);
        Task<OneOf<Success, Failed>> DeleteBasketAsync(Guid id, CancellationToken cancellationToken=default);
        Task<OneOf<Success<Guid>, Failed>> UpdateBasketAsync(BasketEntity basket, CancellationToken cancellationToken=default);
        Task<OneOf<Success<GetBasketByIdResponse>,Failed>> GetBasketByIdAsync(Guid id, CancellationToken cancellationToken=default);
        Task<OneOf<Success<AddProductToBasketResponse>,Failed>> AddProductToBasket(Guid basketId, Guid productId, int amount, CancellationToken cancellationToken=default);
        Task<OneOf<Success<DeleteProductFromBasketResponse>, Failed>> DeleteProductFromBasketAsync(Guid basketId, Guid productId, CancellationToken cancellationToken=default);
        Task<OneOf<Success<UpdateProductInBasketResponse>, Failed>> UpdateProductInBasketAsync(Guid basketId, Guid productId, int amount, CancellationToken cancellation=default);
    }
}
