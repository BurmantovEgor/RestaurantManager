using CSharpFunctionalExtensions;
using PaymentService.Abstractions;
using PaymentService.Data.Entities;
using PaymentService.Messaging;

namespace PaymentService.Services
{
    public class PaymentOrderService : IPaymentOrderService
    {
        IPaymentOrderRepository _repository;
        private readonly KafkaProducer _kafkaProducer;

        public PaymentOrderService(IPaymentOrderRepository repository)
        {
            _repository = repository;
            _kafkaProducer = new KafkaProducer();
        }

        public async Task<Result> CreateOrder(OrderEntity newOrder)
        {
            var result = await _repository.CreateOrder(newOrder);
            if (result.IsFailure) return Result.Failure(result.Error);
            return Result.Success();
        }

        public async Task<Result> UpdatePaymentStatus(Guid orderId)
        {
            var result = await _repository.UpdatePaymentStatus(orderId);
            if (result.IsFailure) return Result.Failure(result.Error);
            await _kafkaProducer.SendPaymentInfo(orderId);
            return result;
        }

        public async Task<Result<List<OrderEntity>>> GetAll()
        {
            var result = await _repository.GetAll();
            return Result.Success(result.Value);
        }
    }
}
