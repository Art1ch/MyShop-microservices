using FluentValidation;

namespace UserService.Application.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is empty")
                .MinimumLength(2).WithMessage("Minimal name length is 2 characters")
                .MaximumLength(30).WithMessage("Maximal name length is 30 characters");

            RuleFor(x => x.Age)
                .NotEmpty().WithMessage("Age is empty")
                .GreaterThanOrEqualTo(18).WithMessage("Minimal age is 18 years")
                .LessThanOrEqualTo(110).WithMessage("Maximal age is 110 years");
        }
    }
}
