using FluentValidation;
using MediatR;
using OneOf;
using Shared.Results;
using System.Text;
using UserService.Application.Contracts.Repository;
using UserService.Application.Responses.CommandsResponses;
using UserService.Core.Entities;

namespace UserService.Application.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, OneOf<Success<UpdateUserResponse>, Failed>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UpdateUserCommand> _validator;

        public UpdateUserCommandHandler(IUserRepository userRepository, IValidator<UpdateUserCommand> validator)
        {
            _userRepository = userRepository;
            _validator = validator;
        }

        public async Task<OneOf<Success<UpdateUserResponse>, Failed>> Handle(UpdateUserCommand request, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var message = new StringBuilder();

                foreach(var error in validationResult.Errors)
                {
                    message.AppendLine(error.ErrorMessage);
                }

                return new Failed(message.ToString());
            }

            var user = new UserEntity()
            {
                Id = request.Id,
                Name = request.Name,
                Password = request.Password,
            };
            return await _userRepository.UpdateUserAsync(user, cancellationToken);
        }
    }
}
