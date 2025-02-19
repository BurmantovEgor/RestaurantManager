using CSharpFunctionalExtensions;
using OrderService.Core;
using OrderService.Data.Entities;

namespace OrderService.Abstractions
{
    public interface IOrderService
    {
        Task<Result> Create(CreateOrderDTO newOrder);
        Task<Result<List<OrderEntity>>> GetAll();


    }
}
