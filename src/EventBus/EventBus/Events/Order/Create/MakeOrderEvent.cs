using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Events.Order.Create
{
    public class MakeOrderEvent
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public MakeOrderEvent(Guid id, decimal price)
        {
            Id = id;
            Price = price;
        }
    }
}
