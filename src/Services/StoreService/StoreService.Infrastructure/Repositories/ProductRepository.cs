using MongoDB.Driver;
using OneOf;
using Shared.Results;
using StoreService.Application.Contracts;
using StoreService.Application.Repsonses.CommandResponses.Product;
using StoreService.Application.Repsonses.QueriesResponses.Product;
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

        public async Task<OneOf<Success<CreateProductResponse>, Failed>> CreateProductAsync(ProductEntity product, CancellationToken cancellationToken = default)
        {
            await _context.Products.InsertOneAsync(product);
            var response = new CreateProductResponse(product.Id);
            return new Success<CreateProductResponse>(response);
        }

        public async Task<OneOf<Success<DeleteProductResponse>, Failed>> DeleteProductAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<ProductEntity>.Filter.Eq("Id", id);
            await _context.Products.DeleteOneAsync(filter, cancellationToken);
            var response = new DeleteProductResponse();
            return new Success<DeleteProductResponse>(response);
        }

        public async Task<OneOf<Success<GetAllProductsResponse>, Failed>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var products = await _context.Products.Find(_ => true).ToListAsync();
            if (products is null)
            {
                return new Failed("There is no products");
            }
            var response = new GetAllProductsResponse(products);
            return new Success<GetAllProductsResponse>(response);
        }

        public async Task<OneOf<Success<GetProductByIdResponse>, Failed>> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<ProductEntity>.Filter.Eq("Id", id);
            var product = await _context.Products.Find(filter).FirstOrDefaultAsync(cancellationToken);
            if (product is null)
            {
                return new Failed("Basket is null");
            }
            var response = new GetProductByIdResponse(product);
            return new Success<GetProductByIdResponse>(response);
        }

        public async Task<OneOf<Success<UpdateProductResponse>, Failed>> UpdateProductAsync(ProductEntity product, CancellationToken cancellationToken = default)
        {
            var updateDefinition = Builders<BasketEntity>.Update
                .Set("Name", product.Name)
                .Set("Description", product.Description)
                .Set("Amount", product.Amount)
                .Set("Price", product.Price)
                .Set("ModifiedTime", DateTime.Now);
            var filter = Builders<BasketEntity>.Filter.Eq("Id", product.Id);
            await _context.Baskets.UpdateOneAsync(filter, updateDefinition, cancellationToken: cancellationToken);
            var response = new UpdateProductResponse(product.Id);
            return new Success<UpdateProductResponse>(response);
        }
    }
}
