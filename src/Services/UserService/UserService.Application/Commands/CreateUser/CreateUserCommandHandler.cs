using EventBus.EventBus.Interface;
using EventBus.Events.User.Create;
using MediatR;
using OneOf;
using Shared.Results;
using UserService.Application.Contracts.Repository;
using UserService.Core.Entities;

namespace UserService.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, OneOf<Success<Guid>, Failed>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventBus _eventBus;

        public CreateUserCommandHandler(IUserRepository userRepository, IEventBus eventBus)
        {
            _userRepository = userRepository;
            _eventBus = eventBus;
        }

        public async Task<OneOf<Success<Guid>, Failed>> Handle(CreateUserCommand request, CancellationToken cancellationToken=default)
        {
            var currentTime = DateTime.UtcNow;
            var user = new UserEntity()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Age = request.Age,
                CreatedTime = currentTime,
                ModifiedTime = currentTime,
            };

            var result = await _userRepository.CreateUserAsync(user, cancellationToken);

            await _eventBus.PublishAsync(new UserCreatedEvent(user.Id), cancellationToken);

            return result;

        }
    }
}
