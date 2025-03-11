using OrderService.Core.Entities;
using OneOf;
using Shared.Results;
using OrderService.Application.Responses.QueriesResponses;

namespace OrderService.Application.Contracts
{
    public interface IOrderRepository
    {
        Task<OneOf<Success<Guid>, Failed>> CreateOrderAsync(OrderEntity orderEntity, CancellationToken cancellationToken=default);
        Task<OneOf<Success<GetAllOrdersResponse>, Failed>> GetAllAsync(CancellationToken cancellationToken=default);
        Task<OneOf<Success, Failed>> DeleteOrderAsync(Guid id, CancellationToken cancelToken=default);
        Task<OneOf<Success<Guid>, Failed>> UpdateOrderAsync(OrderEntity orderEntity, CancellationToken cancellationToken=default);
        Task<OneOf<Success<GetOrderByIdResponse>, Failed>> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken=default);
    }
}
