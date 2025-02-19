using CSharpFunctionalExtensions;
using KitchenService.Abstractions;
using KitchenService.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace KitchenService.Data
{
    public class KitchenRepository : IKitchenRepository
    {
        KitchenDbContext _context;
        public KitchenRepository(KitchenDbContext kitchenDbContext)
        {
            _context = kitchenDbContext;
        }

        public async Task<Result<List<OrderEntity>>> GetAll()
        {
            var result = await _context.Order.Include(d => d.OrderDetails).Include(s => s.Status).ToListAsync();
            return Result.Success(result);
        }

        public async Task<Result> SaveOrder(OrderEntity newOrder)
        {
            var res1 = await _context.Order.ToListAsync();

            await _context.Order.AddAsync(newOrder);
            var result = await _context.SaveChangesAsync();

            var res2 = await _context.Order.ToListAsync();

            Console.WriteLine($"res1 = {res1.Count()} res2 = {res2.Count()}");

            if (result == 0)
            {
                Console.WriteLine("не удалось сохранить в бд");
                return Result.Failure("не удалось сохранить в бд");
            }
            return Result.Success();
        }

        public async Task<Result> UpdatePaymentStatus(Guid orderId)
        {
            var currentOrder = await _context.Order.FirstOrDefaultAsync(x=> x.Id == orderId);
            currentOrder.StatusId = "InProgress";
            var result = await _context.SaveChangesAsync();
            return Result.Success();
        }
    }
}
