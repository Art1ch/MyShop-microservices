using MediatR;
using UserService.Application.Contracts.Repository;
using UserService.Core.Entities;

namespace UserService.Application.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserEntity?>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserEntity?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken = default)
        {
            return await _userRepository.GetUserByIdAsync(request.Id, cancellationToken);
        }
    }
}
