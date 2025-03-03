using MongoDB.Bson;
using MongoDB.Driver;
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
        public async Task<Guid> CreateProductAsync(ProductEntity product, CancellationToken cancellationToken=default)
        {
            await _context.Products.InsertOneAsync(product);
            return product.Id;
        }

        public async Task DeleteProductAsync(Guid id, CancellationToken cancellationToken=default)
        {
            var filter = Builders<ProductEntity>.Filter.Eq("Id", id);

            await _context.Products.DeleteOneAsync(filter, cancellationToken);
        }

        public async Task<List<ProductEntity>> GetAllAsync(CancellationToken cancellationToken=default)
        {
            return await _context.Products.Find(_ => true).ToListAsync();
        }

        public async Task<ProductEntity?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken=default)
        {
            var filter = Builders<ProductEntity>.Filter.Eq("Id", id);

            var product = await _context.Products.Find(filter).FirstOrDefaultAsync(cancellationToken);
            return product;
        }

        public async Task<Guid> UpdateProductAsync(ProductEntity product, CancellationToken cancellationToken=default)
        {
            var updateDefinition = Builders<ProductEntity>.Update
                .Set("Name", product.Name)
                .Set("Description", product.Description)
                .Set("Amount", product.Amount)
                .Set("Price", product.Price)
                .Set("ModifiedTime", DateTime.Now);

            var filter = Builders<ProductEntity>.Filter.Eq("Id", product.Id);

            await _context.Products.UpdateOneAsync(filter, updateDefinition, cancellationToken: cancellationToken);

            return product.Id;
        }
    }
}
