using EventBus.EventBus.Interface;
using EventBus.Events.User.Create;
using FluentValidation;
using MediatR;
using OneOf;
using Shared.Results;
using System.Text;
using UserService.Application.Contracts.Repository;
using UserService.Application.Responses.CommandsResponses;
using UserService.Core.Entities;

namespace UserService.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, OneOf<Success<CreateUserResponse>, Failed>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventBus _eventBus;
        private readonly IValidator<CreateUserCommand> _validator;

        public CreateUserCommandHandler(IUserRepository userRepository, IEventBus eventBus, IValidator<CreateUserCommand> validator)
        {
            _userRepository = userRepository;
            _eventBus = eventBus;
            _validator = validator;
        }

        public async Task<OneOf<Success<CreateUserResponse>, Failed>> Handle(CreateUserCommand request, CancellationToken cancellationToken=default)
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
            var user = new UserEntity()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Password = request.Password,
                CreatedTime = currentTime,
                ModifiedTime = currentTime,
            };

            var result = await _userRepository.CreateUserAsync(user, cancellationToken);

            await _eventBus.PublishAsync(new UserCreatedEvent(user.Id), cancellationToken);

            return result;

        }
    }
}
