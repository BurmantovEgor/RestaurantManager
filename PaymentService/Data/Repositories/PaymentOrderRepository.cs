using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using OrderService.Data.Configurations;
using PaymentService.Abstractions;
using PaymentService.Data.Entities;

namespace PaymentService.Data.Repositories
{
    public class PaymentOrderRepository : IPaymentOrderRepository
    {
        PaymentDbContext _context;

        public PaymentOrderRepository(PaymentDbContext context)
        {
            _context = context;
        }


        public async Task<Result> CreateOrder(OrderEntity newOrder)
        {
            await _context.Order.AddAsync(newOrder);
            var result = await _context.SaveChangesAsync();
            if (result == 0) return Result.Failure("DB Error");
            Console.WriteLine($"Added new Order to PaymentDB id = {newOrder.Id} statusId = {newOrder.StatusId}");
            return Result.Success();
        }

        public async Task<Result<List<OrderEntity>>> GetAll()
        {
            var result = await _context.Order.Include(s => s.Status).ToListAsync();
            return Result.Success(result);
        }

        public async Task<Result> UpdatePaymentStatus(Guid orderId)
        {
            var currentOrder = await _context.Order.FirstOrDefaultAsync(x => x.Id == orderId);
            if (currentOrder == null)
            {
                Console.WriteLine("не удалось получить заказ");
                return Result.Failure("не удалось получить заказ");
            }
            Console.WriteLine("Заказ получены");
            Console.WriteLine($"id = {currentOrder.Id} price = {currentOrder.TotalPrice} status = {currentOrder.StatusId}");
            currentOrder.StatusId = "Paid";
            Console.WriteLine($"id = {currentOrder.Id} price = {currentOrder.TotalPrice} status = {currentOrder.StatusId}");
            var result = await _context.SaveChangesAsync();
            
            
            var currentOrder1 = await _context.Order.FirstOrDefaultAsync(x => x.Id == orderId);
            if (currentOrder1 == null)
            {
                Console.WriteLine("не удалось получить заказ");
                return Result.Failure("не удалось получить заказ");
            }
            Console.WriteLine("Заказ получены");
            Console.WriteLine($"id = {currentOrder1.Id} price = {currentOrder1.TotalPrice} status = {currentOrder1.StatusId}");


            if (result == 0)
            {
                Console.WriteLine("не удалось провести оплату");
                return Result.Failure("не удалось провести оплату");
            }

            Console.WriteLine($"заказ {currentOrder.Id} успешно оплачен");
            return Result.Success();
        }
    }
}
