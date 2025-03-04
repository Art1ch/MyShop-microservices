using MongoDB.Driver;
using OneOf;
using Shared.Results;
using StoreService.Application.Contracts;
using StoreService.Core.Entities;
using StoreService.Infrastructure.Context;

namespace StoreService.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<OneOf<Success<Guid>, Failed>> CreateProductAsync(ProductEntity product, CancellationToken cancellationToken=default)
        {
            await _context.Products.InsertOneAsync(product);
            return new Success<Guid>(product.Id);
        }

        public async Task<OneOf<Success, Failed>> DeleteProductAsync(Guid id, CancellationToken cancellationToken=default)
        {
            var filter = Builders<ProductEntity>.Filter.Eq("Id", id);

            await _context.Products.DeleteOneAsync(filter, cancellationToken);

            return new Success();
        }

        public async Task<OneOf<Success<List<ProductEntity>>, Failed>> GetAllAsync(CancellationToken cancellationToken=default)
        {
            var products = await _context.Products.Find(_ => true).ToListAsync();
            if (products is null) { return new Failed(); }
            return new Success<List<ProductEntity>>(products);
        }

        public async Task<OneOf<Success<ProductEntity>, Failed>> GetProductByIdAsync(Guid id, CancellationToken cancellationToken=default)
        {
            var filter = Builders<ProductEntity>.Filter.Eq("Id", id);

            var product = await _context.Products.Find(filter).FirstOrDefaultAsync(cancellationToken);

            if (product is null) { return new Failed(); }
            return new Success<ProductEntity>(product);
        }

        public async Task<OneOf<Success<Guid>, Failed>> UpdateProductAsync(ProductEntity product, CancellationToken cancellationToken=default)
        {
            var updateDefinition = Builders<ProductEntity>.Update
                .Set("Name", product.Name)
                .Set("Description", product.Description)
                .Set("Amount", product.Amount)
                .Set("Price", product.Price)
                .Set("ModifiedTime", DateTime.Now);

            var filter = Builders<ProductEntity>.Filter.Eq("Id", product.Id);

            await _context.Products.UpdateOneAsync(filter, updateDefinition, cancellationToken: cancellationToken);

            return new Success<Guid>(product.Id);
        }
    }
}
