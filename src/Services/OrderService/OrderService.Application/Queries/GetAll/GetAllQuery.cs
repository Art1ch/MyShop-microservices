using MediatR;
using OneOf;
using OrderService.Core.Entities;
using Shared.Results;

namespace OrderService.Application.Queries.GetAll
{
    public class GetAllQuery : IRequest<OneOf<Success<List<OrderEntity>>, Failed>>
    {
        
    }
}
