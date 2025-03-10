using StoreService.Core.Entities;

namespace StoreService.Application.Repsonses.QueriesResponses.Product
{
    public class GetProductByIdResponse
    {
        public ProductEntity Product { get; set; }
        public GetProductByIdResponse(ProductEntity product)
        {
            Product = product;
        }
    }
}
