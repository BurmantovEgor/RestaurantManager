using CSharpFunctionalExtensions;
using KitchenService.Data;

namespace KitchenService.Abstractions
{
    public interface IKitchenRepository
    {
        Task<Result> SaveOrder(OrderEntity newOrder);
        Task<Result> UpdatePaymentStatus(Guid orderId);
        Task<Result<List<OrderEntity>>> GetAll();

    }
}
