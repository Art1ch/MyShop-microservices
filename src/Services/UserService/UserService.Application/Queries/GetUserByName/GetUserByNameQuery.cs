using MediatR;
using UserService.Core.Entities;

namespace UserService.Application.Queries.GetUserByName
{
    public class GetUserByNameQuery : IRequest<UserEntity?>
    {
        public string Name { get; set; }

        public GetUserByNameQuery(string name)
        {
            Name = name;
        }
    }
}
