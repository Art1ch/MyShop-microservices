using StoreService.Core.Entities;

namespace StoreService.Application.Repsonses.QueriesResponses.Basket
{
    public class GetBasketByIdResponse
    {
        public BasketEntity Basket { get; set; }
        public GetBasketByIdResponse(BasketEntity basket)
        {
            Basket = basket;
        }
    }
}
