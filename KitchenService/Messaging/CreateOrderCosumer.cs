using Confluent.Kafka;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using KitchenService.Data.Configurations;
using KitchenService.Data;
using KitchenService.Abstractions;

namespace KitchenService.Messaging;

public class CreateOrderCosumer : BackgroundService
{
    private readonly string _topic = "orders";
    private readonly IServiceScopeFactory _scopeFactory;

    public CreateOrderCosumer(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "kafka:9092",
            GroupId = "kitchen-group",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };

        using var consumer = new ConsumerBuilder<string, string>(config).Build();
        consumer.Subscribe(_topic);
        await Task.Run(async () =>
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = consumer.Consume(stoppingToken);
                    var message = JsonSerializer.Deserialize<OrderEntity>(consumeResult.Value);

                    if (message != null)
                    {
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var kitchenService = scope.ServiceProvider.GetRequiredService<IKitchenService>();
                            var result = await kitchenService.SaveOrder(message);

                            if (result.IsFailure)
                                throw new Exception(result.Error);

                            consumer.Commit(consumeResult);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка обработки сообщения: {ex.Message}");
                }
            }
        }, stoppingToken);
    }

}
