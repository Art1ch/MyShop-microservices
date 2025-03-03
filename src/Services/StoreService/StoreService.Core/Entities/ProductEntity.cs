namespace StoreService.Core.Entities
{
    public class ProductEntity : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
