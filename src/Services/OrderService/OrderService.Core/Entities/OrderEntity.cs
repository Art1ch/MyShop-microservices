using OrderService.Core.StateEnum;
using Shared.Entities;

namespace OrderService.Core.Entities
{
    public class OrderEntity : BaseEntity
    {
        public decimal Price { get; set; }
        public OrderStateEnum State { get; set; }
    }
}
