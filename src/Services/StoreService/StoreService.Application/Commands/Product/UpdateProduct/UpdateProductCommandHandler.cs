using FluentValidation;
using MediatR;
using OneOf;
using Shared.Results;
using StoreService.Application.Contracts;
using StoreService.Application.Repsonses.CommandResponses.Product;
using StoreService.Core.Entities;
using System.Text;

namespace StoreService.Application.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, OneOf<Success<UpdateProductResponse>, Failed>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidator<UpdateProductCommand> _validator;

        public UpdateProductCommandHandler(IProductRepository productRepository, IValidator<UpdateProductCommand> validator)
        {
            _productRepository = productRepository;
            _validator = validator;
        }

        public async Task<OneOf<Success<UpdateProductResponse>, Failed>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
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

            var product = new ProductEntity()
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Amount = request.Amount,
                Price = request.Price,
            };

            return await _productRepository.UpdateProductAsync(product, cancellationToken);
        }
    }
}
