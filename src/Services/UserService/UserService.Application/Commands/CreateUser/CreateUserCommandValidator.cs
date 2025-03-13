using FluentValidation;

namespace UserService.Application.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is empty")
                .MinimumLength(2).WithMessage("Minimal name length is 2 characters")
                .MaximumLength(30).WithMessage("Maximal name length is 30 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is empty")
                .MinimumLength(8).WithMessage("Minimal name length is 8 characters")
                .MaximumLength(20).WithMessage("Maximal name length is 20 characters");
        }
    }
}
