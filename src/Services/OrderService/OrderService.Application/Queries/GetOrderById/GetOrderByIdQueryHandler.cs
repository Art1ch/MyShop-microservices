using MediatR;
using OrderService.Application.Contracts;
using OrderService.Core.Entities;

namespace OrderService.Application.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderEntity?>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderEntity?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetOrderByIdAsync(request.Id, cancellationToken);
        }
    }
}
