using MediatR;
using OneOf;
using Shared.Results;
using UserService.Core.Entities;

namespace UserService.Application.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<OneOf<Success<UserEntity>, Failed>>
    {
        public Guid Id { get; set; }

        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
