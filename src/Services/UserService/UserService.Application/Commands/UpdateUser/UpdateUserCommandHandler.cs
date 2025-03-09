using MediatR;
using OneOf;
using Shared.Results;
using UserService.Application.Contracts.Repository;
using UserService.Application.Responses.CommandsResponses;
using UserService.Core.Entities;

namespace UserService.Application.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, OneOf<Success<UpdateUserResponse>, Failed>>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<OneOf<Success<UpdateUserResponse>, Failed>> Handle(UpdateUserCommand request, CancellationToken cancellationToken = default)
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
