using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Data.Entities;
using OrderService.Messaging;

namespace OrderService.Controllers;

[Route("orders")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly OrderDbContext _context;
    private readonly KafkaProducer _kafkaProducer;

    public OrderController(OrderDbContext context)
    {
        _context = context;
        _kafkaProducer = new KafkaProducer();
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderEntity order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        await _kafkaProducer.SendOrderCreatedEventAsync(order);
        return Ok(order);
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _context.Orders.ToListAsync();
        return Ok(orders);
    }
}
