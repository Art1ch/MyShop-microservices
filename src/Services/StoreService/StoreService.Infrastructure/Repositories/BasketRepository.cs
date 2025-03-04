using MongoDB.Driver;
using OneOf;
using Shared.Results;
using StoreService.Application.Contracts;
using StoreService.Core.Entities;
using StoreService.Infrastructure.Context;

namespace StoreService.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly StoreContext _context;
        private readonly IProductRepository _productRepository;
        public BasketRepository(StoreContext context, IProductRepository repository)
        {
            _context = context;
            _productRepository = repository;   
        }

        public async Task<OneOf<Success, Failed>> AddProductToBasket(Guid basketId, Guid productId, int amount, CancellationToken cancellationToken)
        {
            var basket = await GetBasketByIdAsync(basketId, cancellationToken);
            if (basket.IsT1) { return new Failed(); }

            var warehouseProduct = await _productRepository.GetProductByIdAsync(productId);
            if (warehouseProduct.IsT1) { return new Failed(); }
            

            var product = new ProductEntity
            {
                Id = basketId,
                Description = warehouseProduct.AsT0.Value!.Description,
                Name = warehouseProduct.AsT0.Value!.Name,
                Amount = amount,
                Price = warehouseProduct.AsT0.Value!.Price,
                CreatedTime = warehouseProduct.AsT0.Value!.CreatedTime,
                ModifiedTime = warehouseProduct.AsT0.Value!.ModifiedTime
            };

            basket.AsT0.Value!.Products.Add(product);
            warehouseProduct.AsT0.Value!.Amount -= amount;

            basket.AsT0.Value!.Price += product.Price * product.Amount; 

            await UpdateBasketAsync(basket.AsT0.Value, cancellationToken);
            await _productRepository.UpdateProductAsync(warehouseProduct.AsT0.Value!, cancellationToken);

            return new Success();
        }

        public Task<OneOf<Success, Failed>> UpdateProductInBasketAsync(Guid basketId, Guid productId, int amount, CancellationToken cancellation = default)
        {
            throw new NotImplementedException(); // I'm too lazy to do this
        }

        public async Task<OneOf<Success, Failed>> DeleteBasketAsync(Guid id, CancellationToken cancellationToken)
        {
            var basket = await GetBasketByIdAsync(id);
            foreach(var product in basket.AsT0.Value!.Products)
            {
                var warehouseProduct = await _productRepository.GetProductByIdAsync(product.Id);
                warehouseProduct.AsT0.Value!.Amount += product.Amount;
            }
            var filter = Builders<BasketEntity>.Filter.Eq("Id", id);

            await _context.Baskets.DeleteOneAsync(filter, cancellationToken);

            return new Success();
        }

        public async Task<OneOf<Success<Guid>, Failed>> CreateBasketAsync(BasketEntity basket, CancellationToken cancellationToken=default)
        {
            await _context.Baskets.InsertOneAsync(basket, cancellationToken: cancellationToken);
            return new Success<Guid>(basket.Id);
        }

        public async Task<OneOf<Success,Failed>> DeleteProductFromBasketAsync(Guid basketId, Guid productId, CancellationToken cancellationToken=default)
        {
            var basket = await GetBasketByIdAsync(basketId, cancellationToken);
            if (basket.IsT1) { return new Failed(); } 
            var warehouseProduct = await _productRepository.GetProductByIdAsync(productId, cancellationToken);
            if (warehouseProduct.IsT1) { return new Failed(); }

            var basketProduct = basket.AsT0.Value!.Products.FirstOrDefault(p => p.Id == productId);
            if (basketProduct is null) { return new Failed();}

            basket.AsT0.Value!.Products.Remove(basketProduct);
            basket.AsT0.Value!.Price -= basketProduct.Price * basketProduct.Amount;

            warehouseProduct.AsT0.Value!.Amount += basketProduct.Amount;

            await UpdateBasketAsync(basket.AsT0.Value!, cancellationToken);
            await _productRepository.UpdateProductAsync(warehouseProduct.AsT0.Value, cancellationToken);

            return new Success();

        }

        public async Task<OneOf<Success<List<BasketEntity>>, Failed>> GetAllAsync(CancellationToken cancellationToken=default)
        {
            var baskets = await _context.Baskets.Find(_ => true).ToListAsync();
            return new Success<List<BasketEntity>>(baskets);
        }

        public async Task<OneOf<Success<BasketEntity>, Failed>> GetBasketByIdAsync(Guid id, CancellationToken cancellationToken=default)
        {
            var filter = Builders<BasketEntity>.Filter.Eq("Id", id);

            var basket = await _context.Baskets.Find(filter).FirstOrDefaultAsync(cancellationToken);

            if (basket is null) { return new Failed(); }

            return new Success<BasketEntity>(basket);
        }

        public async Task<OneOf<Success<Guid>, Failed>> UpdateBasketAsync(BasketEntity basket, CancellationToken cancellationToken=default)
        {
            var updateDefinition = Builders<BasketEntity>.Update
                .Set("Price", basket.Price)
                .Set("Products", basket.Products)
                .Set("ModifiedTime", DateTime.Now);

            var filter = Builders<BasketEntity>.Filter.Eq("Id", basket.Id);

            await _context.Baskets.UpdateOneAsync(filter, updateDefinition, cancellationToken: cancellationToken);

            return new Success<Guid>(basket.Id);
        }

    }
}
