using StoreService.Core.Entities;

namespace StoreService.Application.Repsonses.QueriesResponses.Basket
{
    public class GetAllBasketsResponse
    {
        public List<BasketEntity> Baskets { get; set; }
        public GetAllBasketsResponse(List<BasketEntity> baskets)
        {
            Baskets = baskets;
        }
    }
}
