using MediatR;
using OneOf;
using OrderService.Application.Contracts;
using OrderService.Application.Responses.QueriesResponses;
using OrderService.Core.Entities;
using Shared.Results;

namespace OrderService.Application.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OneOf<Success<GetOrderByIdResponse>, Failed>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OneOf<Success<GetOrderByIdResponse>, Failed>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetOrderByIdAsync(request.Id, cancellationToken);
        }
    }
}
