using MediatR;
using OneOf;
using Shared.Results;
using UserService.Application.Contracts.Repository;
using UserService.Core.Entities;

namespace UserService.Application.Queries.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, OneOf<Success<List<UserEntity>>, Failed>>
    {
        private readonly IUserRepository _userRepository;
        public GetAllQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OneOf<Success<List<UserEntity>>, Failed>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetAllAsync(cancellationToken);
        }
    }
}
