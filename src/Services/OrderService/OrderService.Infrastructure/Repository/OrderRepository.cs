using Microsoft.EntityFrameworkCore;
using OrderService.Application.Contracts;
using OrderService.Core.Entities;
using OrderService.Infrastructure.Context;

namespace OrderService.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _context;

        public OrderRepository(OrderContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateOrderAsync(OrderEntity orderEntity, CancellationToken cancellationToken = default)
        {
            await _context.Orders.AddAsync(orderEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return orderEntity.Id;
        }

        public async Task DeleteOrderAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _context.Orders.Where(o => o.Id == id).ExecuteDeleteAsync(cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<OrderEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Orders.ToListAsync(cancellationToken);
        }

        public async Task<OrderEntity?> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public async Task<Guid> UpdateOrderAsync(OrderEntity orderEntity, CancellationToken cancellationToken = default)
        {
            await _context.Orders
            .Where(o => o.Id == orderEntity.Id)
            .ExecuteUpdateAsync
            (
            s => s.SetProperty(o => o.State, orderEntity.State)
                  .SetProperty(o => o.ModifiedTime, DateTime.UtcNow),
            cancellationToken
            );

            await _context.SaveChangesAsync(cancellationToken);

            return orderEntity.Id;
        }
    }
}
