using OrderService.Core.Entities;

namespace OrderService.Application.Responses.QueriesResponses
{
    public class GetOrderByIdResponse
    {
        public OrderEntity Order { get; set; }

        public GetOrderByIdResponse(OrderEntity order)
        {
            Order = order;
        }
    }
}
