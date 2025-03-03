using MediatR;
using UserService.Core.Entities;

namespace UserService.Application.Queries.GetAll
{
    public class GetAllQuery : IRequest<IReadOnlyList<UserEntity>>;
}
