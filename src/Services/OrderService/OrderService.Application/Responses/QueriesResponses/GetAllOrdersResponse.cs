using OrderService.Core.Entities;

namespace OrderService.Application.Responses.QueriesResponses
{
    public class GetAllOrdersResponse
    {
        public List<OrderEntity> Orders { get; set; }

        public GetAllOrdersResponse(List<OrderEntity> orders)
        {
            Orders = orders;
        }
    }
}
