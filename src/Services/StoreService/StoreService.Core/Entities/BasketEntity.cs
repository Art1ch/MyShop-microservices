using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreService.Core.Entities
{
    public class BasketEntity : EntityBase
    {
        public List<ProductEntity> Products { get; set; }
        public decimal Price { get; set; }
    }
}
