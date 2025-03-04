using MediatR;
using OneOf;
using Shared.Results;

namespace StoreService.Application.Commands.Product.CreateProduct
{
    public class CreateProductCommand : IRequest<OneOf<Success<Guid>, Failed>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
