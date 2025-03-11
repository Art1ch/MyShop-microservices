using MediatR;
using OneOf;
using Shared.Results;
using UserService.Application.Responses.QueriesResponses;

namespace UserService.Application.Queries.GetUserByName
{
    public class GetUserByNameQuery : IRequest<OneOf<Success<GetUserByNameResponse>, Failed>>
    {
        public string Name { get; set; }

        public GetUserByNameQuery(string name)
        {
            Name = name;
        }
    }
}
