using MediatR;

namespace StoreService.Application.Commands.Product.CreateProduct
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
