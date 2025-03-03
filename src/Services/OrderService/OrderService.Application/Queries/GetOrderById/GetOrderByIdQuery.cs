using MediatR;
using OrderService.Core.Entities;

namespace OrderService.Application.Queries.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<OrderEntity?>
    {
        public Guid Id { get; set; }
        public GetOrderByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
