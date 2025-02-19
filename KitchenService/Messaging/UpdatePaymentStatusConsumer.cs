using Confluent.Kafka;
using KitchenService.Abstractions;
using System.Text.Json;

namespace KitchenService.Messaging
{
    public class UpdatePaymentStatusConsumer : BackgroundService
    {
        private readonly string _topic = "payedOrder";
        private readonly IServiceScopeFactory _scopeFactory;

        public UpdatePaymentStatusConsumer(IServiceScopeFactory scopeFactory)
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
                        var message = consumeResult;

                        if (message != null)
                        {

                            Console.WriteLine("123321123312123312213");
                            Console.WriteLine(consumeResult.Value.ToString());
                            Console.WriteLine(consumeResult.Key.ToString());

                            using (var scope = _scopeFactory.CreateScope())
                            {
                                var paymentService = scope.ServiceProvider.GetRequiredService<IKitchenService>();
                                var result = await paymentService.UpdatePaymentStatus(new Guid(message.Key));

                                if (result.IsFailure)
                                    throw new Exception(result.Error);

                                consumer.Commit(consumeResult);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка обработки сообщения из топика 'payments': {ex.Message}");
                    }
                }
            }, stoppingToken);
        }
    }
}
