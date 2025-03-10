using MongoDB.Driver;
using OneOf;
using Shared.Results;
using StoreService.Application.Contracts;
using StoreService.Application.Repsonses.CommandResponses.Basket;
using StoreService.Application.Repsonses.QueriesResponses.Basket;
using StoreService.Core.Entities;
using StoreService.Infrastructure.Context;

namespace StoreService.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly StoreContext _context;
        public BasketRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<OneOf<Success<AddProductToBasketResponse>, Failed>> AddProductToBasket(Guid basketId, Guid productId, int amount, CancellationToken cancellationToken)
        {
            var basketFilter = Builders<BasketEntity>.Filter.Eq("Id", basketId);
            var basket = await _context.Baskets.Find(basketFilter).FirstOrDefaultAsync(cancellationToken);

            if (basket is null) 
            {
                return new Failed("Basket is null");
            }

            var productFilter = Builders<ProductEntity>.Filter.Eq("Id", productId);
            var product = await _context.Products.Find(productFilter).FirstOrDefaultAsync(cancellationToken);

            if (product is null)
            {
                return new Failed("Product is null");
            }

            product.Amount = amount;
            basket.Products.Add(product);
            var price = product.Price * product.Amount;

            var updateDefinition = Builders<BasketEntity>.Update
                .Set("Products", basket.Products)
                .Set("Price", basket.Price + price)
                .Set("ModifiedTime", DateTime.Now);

            await _context.Baskets.UpdateOneAsync(basketFilter, updateDefinition, cancellationToken: cancellationToken);

            var response = new AddProductToBasketResponse();

            return new Success<AddProductToBasketResponse>(response);
        }

        public async Task<OneOf<Success<Guid>, Failed>> CreateBasketAsync(BasketEntity basket, CancellationToken cancellationToken)
        {
            await _context.Baskets.InsertOneAsync(basket);
            return new Success<Guid>(basket.Id);
        }

        public async Task<OneOf<Success, Failed>> DeleteBasketAsync(Guid id, CancellationToken cancellationToken)
        {
            var filter = Builders<BasketEntity>.Filter.Eq("Id", id);
            await _context.Baskets.DeleteOneAsync(filter, cancellationToken);
            return new Success();
        }

        public async Task<OneOf<Success<DeleteProductFromBasketResponse>, Failed>> DeleteProductFromBasketAsync(Guid basketId, Guid productId, CancellationToken cancellationToken)
        {
            var basketFilter = Builders<BasketEntity>.Filter.Eq("Id", basketId);
            var basket = await _context.Baskets.Find(basketFilter).FirstOrDefaultAsync(cancellationToken);

            if (basket is null)
            {
                return new Failed("Basket is null");
            }

            var product = basket.Products.FirstOrDefault(p => p.Id == productId);
            if (product is null)
            {
                return new Failed("Product is null");
            }

            basket.Products.Remove(product);
            var price = product.Price + product.Amount;

            var updateDefinition = Builders<BasketEntity>.Update
                .Set("Products", basket.Products)
                .Set("Price", basket.Price - price)
                .Set("ModifiedTime", DateTime.Now);

            await _context.Baskets.UpdateOneAsync(basketFilter, updateDefinition, cancellationToken: cancellationToken);

            var response = new DeleteProductFromBasketResponse();

            return new Success<DeleteProductFromBasketResponse>(response);
        }


        public async Task<OneOf<Success<GetAllBasketsResponse>, Failed>> GetAllAsync(CancellationToken cancellationToken)
        {
            var baskets = await _context.Baskets.Find(_ => true).ToListAsync();
            if (baskets is null) 
            {
                return new Failed("There is no baskets");
            }
            var response = new GetAllBasketsResponse(baskets);
            return new Success<GetAllBasketsResponse>(response);
        }

        public async Task<OneOf<Success<GetBasketByIdResponse>, Failed>> GetBasketByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var filter = Builders<BasketEntity>.Filter.Eq("Id", id);
            var basket = await _context.Baskets.Find(filter).FirstOrDefaultAsync(cancellationToken);
            if (basket is null)
            { 
                return new Failed("Basket is null");
            }
            var response = new GetBasketByIdResponse(basket);
            return new Success<GetBasketByIdResponse>(response);
        }

        public async Task<OneOf<Success<Guid>, Failed>> UpdateBasketAsync(BasketEntity basket, CancellationToken cancellationToken)
        {
            var updateDefinition = Builders<BasketEntity>.Update
                .Set("Products", basket.Products)
                .Set("Price", basket.Price)
                .Set("ModifiedTime", DateTime.Now);
            var filter = Builders<BasketEntity>.Filter.Eq("Id", basket.Id);
            await _context.Baskets.UpdateOneAsync(filter, updateDefinition, cancellationToken: cancellationToken);
            return new Success<Guid>(basket.Id);
        }

        public async Task<OneOf<Success<UpdateProductInBasketResponse>, Failed>> UpdateProductInBasketAsync(Guid basketId, Guid productId, int amount, CancellationToken cancellationToken)
        {
            var basketFilter = Builders<BasketEntity>.Filter.Eq("Id", basketId);
            var basket = await _context.Baskets.Find(basketFilter).FirstOrDefaultAsync(cancellationToken);

            if (basket is null)
            {
                return new Failed("Basket is null");
            }

            var product = basket.Products.FirstOrDefault(p => p.Id == productId);
            if (product is null)
            {
                return new Failed("Product is null");
            }

            basket.Products.Remove(product);
            product.Amount = amount;
            var price = product.Price + product.Amount + basket.Price;
            basket.Products.Add(product);   

            var updateDefinition = Builders<BasketEntity>.Update
                .Set("Products", basket.Products)
                .Set("Price", price)
                .Set("ModifiedTime", DateTime.Now);

            await _context.Baskets.UpdateOneAsync(basketFilter, updateDefinition, cancellationToken: cancellationToken);

            var response = new UpdateProductInBasketResponse();

            return new Success<UpdateProductInBasketResponse>(response);
        }
    }
}
