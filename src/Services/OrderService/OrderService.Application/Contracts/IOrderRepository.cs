using OrderService.Core.Entities;

namespace OrderService.Application.Contracts
{
    public interface IOrderRepository
    {
        Task<Guid> CreateOrderAsync(OrderEntity orderEntity, CancellationToken cancellationToken=default);
        Task<List<OrderEntity>> GetAllAsync(CancellationToken cancellationToken=default);
        Task DeleteOrderAsync(Guid id, CancellationToken cancelToken=default);
        Task<Guid> UpdateOrderAsync(OrderEntity orderEntity, CancellationToken cancellationToken=default);
        Task<OrderEntity?> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken=default);
    }
}
