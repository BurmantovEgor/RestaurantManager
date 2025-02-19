using CSharpFunctionalExtensions;
using KitchenService.Abstractions;
using KitchenService.Data;

namespace KitchenService.Services
{
    public class KitchenService : IKitchenService
    {
        IKitchenRepository _kitchenRepository;
        public KitchenService(IKitchenRepository kitchenRepository)
        {
            _kitchenRepository = kitchenRepository;
        }

        public async Task<Result<List<OrderEntity>>> GetAll()
        {
            var result = await _kitchenRepository.GetAll();
            return result;
        }

        public async Task<Result> SaveOrder(OrderEntity newOrder)
        {
            newOrder.StatusId = "WaitPay";
            var result = await _kitchenRepository.SaveOrder(newOrder);
            if (result.IsFailure)
            {
                Console.WriteLine("не удалось сохранить в бд");
                return Result.Failure(result.Error);

            }
            return Result.Success();
        }

        public async Task<Result> UpdatePaymentStatus(Guid orderId)
        {
            var result = await _kitchenRepository.UpdatePaymentStatus(orderId);
            return result;

        }
    }
}
