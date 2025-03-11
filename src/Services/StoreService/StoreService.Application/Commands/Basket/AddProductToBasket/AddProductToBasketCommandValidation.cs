using FluentValidation;
using StoreService.Application.Commands.Basket.AddProduct;

namespace StoreService.Application.Commands.Basket.AddProductToBasket
{
    public class AddProductToBasketCommandValidation : AbstractValidator<AddProductToBasketCommand>
    {
        public AddProductToBasketCommandValidation()
        {
            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Amount is needed")
                .GreaterThanOrEqualTo(1).WithMessage("Minimal value of amount is 1")
                .LessThanOrEqualTo(999).WithMessage("Incorrect amount");
        }
    }
}
