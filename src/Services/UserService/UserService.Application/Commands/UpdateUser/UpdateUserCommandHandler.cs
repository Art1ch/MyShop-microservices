using MediatR;
using UserService.Application.Contracts.Repository;
using UserService.Core.Entities;

namespace UserService.Application.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken = default)
        {
            var user = new UserEntity()
            {
                Id = request.Id,
                Name = request.Name,
                Age = request.Age,
            };
            return await _userRepository.UpdateUserAsync(user, cancellationToken);
        }
    }
}
