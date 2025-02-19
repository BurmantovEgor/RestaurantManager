using Confluent.Kafka;
using StackExchange.Redis;

namespace PaymentService.Messaging
{
    public class KafkaProducer
    {
        private readonly string _topic = "payedOrder";
        private readonly IProducer<string, string> _producer;
        public KafkaProducer()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "kafka:9092",
                Partitioner = Partitioner.Random,
                Acks = Acks.All,
                EnableIdempotence = true
            };
            _producer = new ProducerBuilder<string, string>(config).Build();
            Console.WriteLine("kafka создана");

        }

        public async Task SendPaymentInfo(Guid id)
        {
            int maxRetries = 5;
            int retryCount = 0;

            while (retryCount < maxRetries)
            {
                Console.WriteLine("попытка 1");

                try
                {
                    var deliveryResult = await _producer.ProduceAsync(_topic, new Message<string, string>
                    {
                        Key = id.ToString(),
                        Value = "Paid"
                    });

                    if (deliveryResult.Status == PersistenceStatus.Persisted)
                    {
                        Console.WriteLine($"Message with key {id} successfully delivered to {deliveryResult.TopicPartitionOffset}");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Message with key {id} delivery failed. Status: {deliveryResult.Status}");
                    }
                }
                catch (ProduceException<string, string> ex)
                {
                    Console.WriteLine($"Error occurred while sending message: {ex.Error.Reason}");
                }

                retryCount++;
                if (retryCount < maxRetries)
                {
                    Console.WriteLine($"Retrying... Attempt {retryCount}/{maxRetries}");
                    await Task.Delay(1000);
                }
                else
                {
                    Console.WriteLine("Max retry attempts reached. Message could not be delivered.");
                }
            }

        }
    }
}
