using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Abstractions;
using OrderService.Core;
using OrderService.Data.Configurations;
using OrderService.Data.Entities;
using OrderService.Messaging;

namespace OrderService.Controllers;

[Route("orders")]
[ApiController]
public class OrderController : ControllerBase
{
    IOrderService _orderService;
 //   private readonly OrderDbContext _context;
//    private readonly KafkaProducer _kafkaProducer;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    //    _context = context;
//        _kafkaProducer = new KafkaProducer();
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO order)
    {
        var result = await _orderService.Create(order);

     //   _context.Order.Add(order);
     //   await _context.SaveChangesAsync();

//        await _kafkaProducer.SendOrderCreatedEventAsync(order);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _orderService.GetAll();
        return Ok(orders.Value);
    }
}
