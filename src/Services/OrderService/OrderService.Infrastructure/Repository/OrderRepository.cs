using Microsoft.EntityFrameworkCore;
using OneOf;
using OrderService.Application.Contracts;
using OrderService.Application.Responses.QueriesResponses;
using OrderService.Core.Entities;
using OrderService.Infrastructure.Context;
using Shared.Results;

namespace OrderService.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _context;

        public OrderRepository(OrderContext context)
        {
            _context = context;
        }

        public async Task<OneOf<Success<Guid>, Failed>> CreateOrderAsync(OrderEntity orderEntity, CancellationToken cancellationToken = default)
        {
            await _context.Orders.AddAsync(orderEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return new Success<Guid>(orderEntity.Id);
        }

        public async Task<OneOf<Success, Failed>> DeleteOrderAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _context.Orders.Where(o => o.Id == id).ExecuteDeleteAsync(cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return new Success();
        }

        public async Task<OneOf<Success<GetAllOrdersResponse>, Failed>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var orders = await _context.Orders.ToListAsync(cancellationToken);
            if (orders is null)
            { 
                return new Failed();
            }
            var response = new GetAllOrdersResponse(orders);
            return new Success<GetAllOrdersResponse>(response);
        }

        public async Task<OneOf<Success<GetOrderByIdResponse>, Failed>> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
            if (order is null) 
            { 
                return new Failed();
            }
            var response = new GetOrderByIdResponse(order);
            return new Success<GetOrderByIdResponse>(response);
        }

        public async Task<OneOf<Success<Guid>, Failed>> UpdateOrderAsync(OrderEntity orderEntity, CancellationToken cancellationToken = default)
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

            return new Success<Guid>(orderEntity.Id);
        }
    }
}
