using MediatR;
using OneOf;
using Shared.Results;
using UserService.Application.Responses.QueriesResponses;
using UserService.Core.Entities;

namespace UserService.Application.Queries.GetAll
{
    public class GetAllQuery : IRequest<OneOf<Success<GetAllResponse>, Failed>>
    {

    }
}
