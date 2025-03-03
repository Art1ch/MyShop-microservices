using MediatR;
using OrderService.Application.Contracts;
using OrderService.Core.Entities;

namespace OrderService.Application.Queries.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, List<OrderEntity>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetAllQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderEntity>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetAllAsync(cancellationToken);
        }
    }
}
