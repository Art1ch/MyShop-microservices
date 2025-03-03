using MediatR;
using OrderService.Core.Entities;

namespace OrderService.Application.Queries.GetAll
{
    public class GetAllQuery : IRequest<List<OrderEntity>>
    {
        
    }
}
