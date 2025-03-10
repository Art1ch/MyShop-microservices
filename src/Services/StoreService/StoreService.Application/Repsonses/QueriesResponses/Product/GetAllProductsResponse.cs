using StoreService.Core.Entities;

namespace StoreService.Application.Repsonses.QueriesResponses.Product
{
    public class GetAllProductsResponse
    {
        public List<ProductEntity> Products { get; set; }
        public GetAllProductsResponse(List<ProductEntity> products)
        {
            Products = products;
        }
    }
}
