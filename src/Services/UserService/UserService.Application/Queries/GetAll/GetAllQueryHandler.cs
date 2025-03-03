using MediatR;
using UserService.Application.Contracts.Repository;
using UserService.Core.Entities;

namespace UserService.Application.Queries.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, IReadOnlyList<UserEntity>>
    {
        private readonly IUserRepository _userRepository;
        public GetAllQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IReadOnlyList<UserEntity>> Handle(GetAllQuery request, CancellationToken cancellationToken = default)
        {
            return await _userRepository.GetAllAsync(cancellationToken);
        }
    }
}
