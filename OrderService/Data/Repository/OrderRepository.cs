using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using OrderService.Abstractions;
using OrderService.Data.Configurations;
using OrderService.Data.Entities;

namespace OrderService.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        OrderDbContext _context;

        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Guid>> Create(OrderEntity newOrder)
        {
            await _context.Order.AddAsync(newOrder);
            var result = await _context.SaveChangesAsync();
            return Result.Success(newOrder.Id);

        }

        public async Task<Result<List<OrderEntity>>> GetAll()
        {
            var result = await _context.Order.Include(x => x.OrderDetails).Include(s => s.Status).ToListAsync();
            return Result.Success(result);
        }
    }
}
