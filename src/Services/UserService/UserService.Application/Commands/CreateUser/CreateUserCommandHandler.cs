using EventBus.EventBus.Interface;
using EventBus.Events.User.Create;
using MediatR;
using UserService.Application.Contracts.Repository;
using UserService.Core.Entities;

namespace UserService.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventBus _eventBus;

        public CreateUserCommandHandler(IUserRepository userRepository, IEventBus eventBus)
        {
            _userRepository = userRepository;
            _eventBus = eventBus;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken=default)
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
