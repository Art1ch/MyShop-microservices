using MediatR;
using OneOf;
using Shared.Results;
using UserService.Core.Entities;

namespace UserService.Application.Queries.GetUserByName
{
    public class GetUserByNameQuery : IRequest<OneOf<Success<UserEntity>, Failed>>
    {
        public string Name { get; set; }

        public GetUserByNameQuery(string name)
        {
            Name = name;
        }
    }
}
