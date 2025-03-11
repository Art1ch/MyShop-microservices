using FluentValidation;

namespace StoreService.Application.Commands.Product.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name can't be empty")
                .MinimumLength(2).WithMessage("Minimal length of name is 2 characters")
                .MaximumLength(40).WithMessage("Maximal length of name is 40 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description can't be empty")
                .MinimumLength(10).WithMessage("Minimal length of description is 10 characters")
                .MaximumLength(100).WithMessage("Maximal length of description is 100 characters");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price can't be empty")
                .GreaterThanOrEqualTo(0).WithMessage("Price can't be 0")
                .LessThanOrEqualTo(decimal.MaxValue).WithMessage("Incorrect price");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Amount is needed")
                .GreaterThanOrEqualTo(1).WithMessage("Minimal value of amount is 1")
                .LessThanOrEqualTo(999).WithMessage("Incorrect amount");
        }
    }
}
