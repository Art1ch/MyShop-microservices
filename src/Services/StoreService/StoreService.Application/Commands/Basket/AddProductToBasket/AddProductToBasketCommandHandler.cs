using FluentValidation;
using MediatR;
using OneOf;
using Shared.Results;
using StoreService.Application.Commands.Basket.AddProduct;
using StoreService.Application.Contracts;
using StoreService.Application.Repsonses.CommandResponses.Basket;
using System.Text;

namespace StoreService.Application.Commands.Basket.AddProductToBasket
{
    public class AddProductToBasketCommandHandler : IRequestHandler<AddProductToBasketCommand, OneOf<Success<AddProductToBasketResponse>, Failed>>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IValidator<AddProductToBasketCommand> _validator;

        public AddProductToBasketCommandHandler(IBasketRepository basketRepository, IValidator<AddProductToBasketCommand> validator)
        {
            _basketRepository = basketRepository;
            _validator = validator;
        }

        public async Task<OneOf<Success<AddProductToBasketResponse>, Failed>> Handle(AddProductToBasketCommand request, CancellationToken cancellationToken)
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

            return await _basketRepository.AddProductToBasket(request.BasketId, request.ProductId, request.Amount, cancellationToken);
        }
    }
}
