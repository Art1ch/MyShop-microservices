using OrderService.Core.StateEnum;

namespace OrderService.Core.Entities
{
    public class OrderEntity : EntityBase
    {
        public decimal Price { get; set; }
        public OrderStateEnum State { get; set; }
    }
}
