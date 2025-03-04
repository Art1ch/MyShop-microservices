using MediatR;
using OneOf;
using Shared.Results;

namespace StoreService.Application.Commands.Product.UpdateProduct
{
    public class UpdateProductCommand : IRequest<OneOf<Success<Guid>, Failed>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
