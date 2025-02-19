using CSharpFunctionalExtensions;
using PaymentService.Data.Entities;

namespace PaymentService.Abstractions
{
    public interface IPaymentOrderService
    {
        Task<Result> CreateOrder(OrderEntity newOrder);
        Task<Result> UpdatePaymentStatus(Guid orderId);
        Task<Result<List<OrderEntity>>> GetAll();

    }
}
