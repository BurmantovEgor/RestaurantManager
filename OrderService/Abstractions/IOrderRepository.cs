using CSharpFunctionalExtensions;
using OrderService.Core;
using OrderService.Data.Entities;

namespace OrderService.Abstractions
{
    public interface IOrderRepository
    {
        Task<Result<Guid>> Create(OrderEntity newOrder);
        Task<Result<List<OrderEntity>>> GetAll();

    }
}
