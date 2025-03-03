using MediatR;
using UserService.Core.Entities;

namespace UserService.Application.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserEntity?>
    {
        public Guid Id { get; set; }

        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
