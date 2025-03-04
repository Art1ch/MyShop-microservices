using OrderService.Core.Entities;
using OneOf;
using Shared.Results;

namespace OrderService.Application.Contracts
{
    public interface IOrderRepository
    {
        Task<OneOf<Success<Guid>, Failed>> CreateOrderAsync(OrderEntity orderEntity, CancellationToken cancellationToken=default);
        Task<OneOf<Success<List<OrderEntity>>, Failed>> GetAllAsync(CancellationToken cancellationToken=default);
        Task<OneOf<Success, Failed>> DeleteOrderAsync(Guid id, CancellationToken cancelToken=default);
        Task<OneOf<Success<Guid>, Failed>> UpdateOrderAsync(OrderEntity orderEntity, CancellationToken cancellationToken=default);
        Task<OneOf<Success<OrderEntity>, Failed>> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken=default);
    }
}
