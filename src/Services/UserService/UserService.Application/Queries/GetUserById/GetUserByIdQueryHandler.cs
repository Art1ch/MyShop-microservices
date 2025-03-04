using MediatR;
using OneOf;
using Shared.Results;
using UserService.Application.Contracts.Repository;
using UserService.Core.Entities;

namespace UserService.Application.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, OneOf<Success<UserEntity>, Failed>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OneOf<Success<UserEntity>, Failed>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUserByIdAsync(request.Id, cancellationToken);
        }
    }
}
