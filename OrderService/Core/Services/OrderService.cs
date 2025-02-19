using AutoMapper;
using CSharpFunctionalExtensions;
using OrderService.Abstractions;
using OrderService.Data.Entities;
using OrderService.Messaging;

namespace OrderService.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mappper;
        private readonly KafkaProducer _kafkaProducer;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mappper = mapper;
            _kafkaProducer = new KafkaProducer();
        }
        public async Task<Result> Create(CreateOrderDTO order)
        {
            var orderEntity = _mappper.Map<OrderEntity>(order);
            orderEntity.StatusId = new Guid("ff5d8e82-ddce-487b-8c18-3150f1d459a4");
            var resultCreateOrder = await _orderRepository.Create(orderEntity);
            if (resultCreateOrder.IsFailure) return Result.Failure(resultCreateOrder.Error);

            await _kafkaProducer.SendOrderCreatedEventAsync(orderEntity);

            return Result.Success();
        }

        public async Task<Result<List<OrderEntity>>> GetAll()
        {

            var result = await _orderRepository.GetAll();
            return Result.Success(result.Value);

        }
    }
}
