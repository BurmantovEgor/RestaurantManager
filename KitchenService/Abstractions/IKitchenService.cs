using CSharpFunctionalExtensions;
using KitchenService.Data;

namespace KitchenService.Abstractions
{
    public interface IKitchenService
    {
        Task<Result> SaveOrder(OrderEntity newOrder);
        Task<Result> UpdatePaymentStatus(Guid orderId);
        Task<Result<List<OrderEntity>>> GetAll();

    }
}
