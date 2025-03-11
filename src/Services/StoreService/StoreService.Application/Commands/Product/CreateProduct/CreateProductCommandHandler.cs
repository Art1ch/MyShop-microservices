using FluentValidation;
using MediatR;
using OneOf;
using Shared.Results;
using StoreService.Application.Contracts;
using StoreService.Application.Repsonses.CommandResponses.Product;
using StoreService.Core.Entities;
using System.Text;

namespace StoreService.Application.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, OneOf<Success<CreateProductResponse>, Failed>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidator<CreateProductCommand> _validator;

        public CreateProductCommandHandler(IProductRepository productRepository, IValidator<CreateProductCommand> validator)
        {
            _productRepository = productRepository;
            _validator = validator;
        }

        public async Task<OneOf<Success<CreateProductResponse>, Failed>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var message = new StringBuilder();
                foreach (var error in validationResult.Errors)
                {
                    message.AppendLine(error.ErrorMessage);
                }

                return new Failed(message.ToString());
            }

            var currentTime = DateTime.UtcNow;
            var product = new ProductEntity()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Amount = request.Amount,
                Price = request.Price,
                CreatedTime = currentTime,
                ModifiedTime = currentTime
            };

            return await _productRepository.CreateProductAsync(product, cancellationToken);
        }
    }
}
