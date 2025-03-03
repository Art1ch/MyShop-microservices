using Shared.Entities;

namespace StoreService.Core.Entities
{
    public class BasketEntity : BaseEntity
    {
        public List<ProductEntity> Products { get; set; }
        public decimal Price { get; set; }
    }
}
