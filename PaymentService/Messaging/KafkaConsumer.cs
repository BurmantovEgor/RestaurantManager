using Confluent.Kafka;
using PaymentService.Abstractions;
using PaymentService.Data.Entities;
using System.Text.Json;

namespace PaymentService.Messaging
{
    public class KafkaConsumer : BackgroundService
    {
        private readonly string _topic = "orders";
        private readonly IServiceScopeFactory _scopeFactory;

        public KafkaConsumer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "kafka:9092",
                GroupId = "payment-group",
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
                            message.StatusId = "NotPaid";
                            using (var scope = _scopeFactory.CreateScope())
                            {
                                var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentOrderService>();
                                var result = await paymentService.CreateOrder(message);

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
}
