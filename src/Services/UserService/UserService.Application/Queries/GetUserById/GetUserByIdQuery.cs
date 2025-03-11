using MediatR;
using OneOf;
using Shared.Results;
using UserService.Application.Responses.QueriesResponses;
using UserService.Core.Entities;

namespace UserService.Application.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<OneOf<Success<GetUserByIdResponse>, Failed>>
    {
        public Guid Id { get; set; }

        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
