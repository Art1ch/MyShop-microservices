using MediatR;
using OneOf;
using Shared.Results;
using UserService.Core.Entities;

namespace UserService.Application.Queries.GetAll
{
    public class GetAllQuery : IRequest<OneOf<Success<List<UserEntity>>, Failed>>
    {

    }
}
