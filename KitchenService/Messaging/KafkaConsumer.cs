using Confluent.Kafka;
using System.Text.Json;
using KitchenService.Data;
using Microsoft.EntityFrameworkCore;
using KitchenService.Models;

namespace KitchenService.Messaging;

public class KafkaConsumer : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly string _topic = "orders";
    private readonly KitchenDbContext _context;

    public KafkaConsumer(IServiceScopeFactory scopeFactory, KitchenDbContext context)
    {
        _scopeFactory = scopeFactory;
        _context = context;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "kafka:9092",
            GroupId = "kitchen-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<string, string>(config).Build();
        consumer.Subscribe(_topic);

        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("1");

            try
            {
                Console.WriteLine("2");
                var consumeResult = consumer.Consume(stoppingToken);
                var message = JsonSerializer.Deserialize<Order>(consumeResult.Value);
                Console.WriteLine(message.ToString());
                Console.WriteLine("3");

                if (message != null)
                {

                    await ProcessOrderAsync(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка обработки сообщения: {ex.Message}");
            }
        }
    }

    private async Task ProcessOrderAsync(Order order)
    {

        if (order != null)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            Console.WriteLine($"Заказ {order.Id} передан на кухню.");


            var result = await _context.Orders.FirstOrDefaultAsync(x=> x.Id == order.Id);
            if(result != null)
            {
                Console.WriteLine($"Заказ найден в базе");

            }
            else
            {
                Console.WriteLine($"Заказа нет");

            }
        }
        else
        {
            Console.WriteLine($"Заказ {order.Id} не найден.");
        }
    }
}

public class OrderEvent
{
    public Guid OrderId { get; set; }
    public string Event { get; set; }
}
