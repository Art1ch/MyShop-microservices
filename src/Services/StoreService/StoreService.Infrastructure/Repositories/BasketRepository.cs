using MongoDB.Driver;
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

        public async Task AddProductToBasket(Guid basketId, Guid productId, int amount, CancellationToken cancellationToken)
        {
            var basket = await GetBasketByIdAsync(basketId, cancellationToken);
            if (basket is null) { return; } // Raise Exception

            var warehouseProduct = await _productRepository.GetProductByIdAsync(productId);
            if (warehouseProduct is null) { return; } // Raise Exception

            var product = new ProductEntity
            {
                Id = basketId,
                Description = warehouseProduct.Description,
                Name = warehouseProduct.Name,
                Amount = amount,
                Price = warehouseProduct.Price,
                CreatedTime = warehouseProduct.CreatedTime,
                ModifiedTime = warehouseProduct.ModifiedTime
            };

            basket.Products.Add(product);
            warehouseProduct.Amount -= amount;

            basket.Price += product.Price * product.Amount; 

            await UpdateBasketAsync(basket, cancellationToken);
            await _productRepository.UpdateProductAsync(warehouseProduct, cancellationToken);
        }

        public Task UpdateProductInBasketAsync(Guid basketId, Guid productId, int amount, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteBasketAsync(Guid id, CancellationToken cancellationToken)
        {
            var basket = await GetBasketByIdAsync(id);
            foreach(var product in basket.Products)
            {
                var warehouseProduct = await _productRepository.GetProductByIdAsync(product.Id);
                warehouseProduct.Amount += product.Amount;
            }
            var filter = Builders<BasketEntity>.Filter.Eq("Id", id);

            await _context.Baskets.DeleteOneAsync(filter, cancellationToken);
        }

        public async Task<Guid> CreateBasketAsync(BasketEntity basket, CancellationToken cancellationToken=default)
        {
            await _context.Baskets.InsertOneAsync(basket, cancellationToken: cancellationToken);
            return basket.Id;
        }

        public async Task DeleteProductFromBasketAsync(Guid basketId, Guid productId, CancellationToken cancellationToken=default)
        {
            var basket = await GetBasketByIdAsync(basketId, cancellationToken);
            if (basket is null) { return; } // Raise Exception
            var warehouseProduct = await _productRepository.GetProductByIdAsync(productId, cancellationToken);
            if (warehouseProduct is null) { return; } // Raise Exception

            var basketProduct = basket.Products.FirstOrDefault(p => p.Id == productId);
            if (basketProduct is null) { return;} // Raise Exception

            basket.Products.Remove(basketProduct);
            basket.Price -= basketProduct.Price * basketProduct.Amount;

            warehouseProduct.Amount += basketProduct.Amount;

            await UpdateBasketAsync(basket, cancellationToken);
            await _productRepository.UpdateProductAsync(warehouseProduct, cancellationToken);

        }

        public async Task<List<BasketEntity>> GetAllAsync(CancellationToken cancellationToken=default)
        {
            return await _context.Baskets.Find(_ => true).ToListAsync();
        }

        public async Task<BasketEntity?> GetBasketByIdAsync(Guid id, CancellationToken cancellationToken=default)
        {
            var filter = Builders<BasketEntity>.Filter.Eq("Id", id);

            var basket = await _context.Baskets.Find(filter).FirstOrDefaultAsync(cancellationToken);

            return basket;
        }

        public async Task<Guid> UpdateBasketAsync(BasketEntity basket, CancellationToken cancellationToken=default)
        {
            var updateDefinition = Builders<BasketEntity>.Update
                .Set("Price", basket.Price)
                .Set("Products", basket.Products)
                .Set("ModifiedTime", DateTime.Now);

            var filter = Builders<BasketEntity>.Filter.Eq("Id", basket.Id);

            await _context.Baskets.UpdateOneAsync(filter, updateDefinition, cancellationToken: cancellationToken);

            return basket.Id;
        }

    }
}
