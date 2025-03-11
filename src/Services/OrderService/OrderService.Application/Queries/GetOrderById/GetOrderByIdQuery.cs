using MediatR;
using OneOf;
using OrderService.Application.Responses.QueriesResponses;
using OrderService.Core.Entities;
using Shared.Results;

namespace OrderService.Application.Queries.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<OneOf<Success<GetOrderByIdResponse>, Failed>>
    {
        public Guid Id { get; set; }
        public GetOrderByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
