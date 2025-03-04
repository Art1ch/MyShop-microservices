using MediatR;
using OneOf;
using Shared.Results;
using UserService.Application.Contracts.Repository;
using UserService.Core.Entities;

namespace UserService.Application.Queries.GetUserByName
{
    public class GetUserByNameQueryHandler : IRequestHandler<GetUserByNameQuery, OneOf<Success<UserEntity>, Failed>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByNameQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OneOf<Success<UserEntity>, Failed>> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUserByNameAsync(request.Name, cancellationToken);
        }
    }
}
