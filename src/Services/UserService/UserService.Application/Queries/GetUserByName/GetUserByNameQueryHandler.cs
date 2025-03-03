using MediatR;
using UserService.Application.Contracts.Repository;
using UserService.Core.Entities;

namespace UserService.Application.Queries.GetUserByName
{
    public class GetUserByNameQueryHandler : IRequestHandler<GetUserByNameQuery, UserEntity?>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByNameQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserEntity?> Handle(GetUserByNameQuery request, CancellationToken cancellationToken = default)
        {
            return await _userRepository.GetUserByNameAsync(request.Name, cancellationToken);
        }
    }
}
