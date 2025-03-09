using MediatR;
using OneOf;
using Shared.Results;
using UserService.Application.Contracts.Repository;
using UserService.Application.Responses.CommandsResponses;

namespace UserService.Application.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, OneOf<Success<DeleteUserResponse>, Failed>>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<OneOf<Success<DeleteUserResponse>, Failed>> Handle(DeleteUserCommand request, CancellationToken cancellationToken = default)
        {
            return await _userRepository.DeleteUserAsync(request.Id, cancellationToken);
        }
    }
}
