using MediatR;
using OneOf;
using OrderService.Application.Contracts;
using OrderService.Application.Responses.QueriesResponses;
using OrderService.Core.Entities;
using Shared.Results;

namespace OrderService.Application.Queries.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, OneOf<Success<GetAllOrdersResponse>, Failed>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetAllQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OneOf<Success<GetAllOrdersResponse>, Failed>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetAllAsync(cancellationToken);
        }
    }
}
