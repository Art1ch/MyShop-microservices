using MediatR;
using UserService.Application.Contracts.Repository;

namespace UserService.Application.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken = default)
        {
            
            await _userRepository.DeleteUserAsync(request.Id, cancellationToken);
        }
    }
}
