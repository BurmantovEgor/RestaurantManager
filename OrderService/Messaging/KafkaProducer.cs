using Confluent.Kafka;
using OrderService.Data.Entities;
using System.Text.Json;

namespace OrderService.Messaging;

public class KafkaProducer
{
    private readonly string _topic = "orders";
    private readonly IProducer<string, string> _producer;

    public KafkaProducer()
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "kafka:9092",
            Partitioner = Partitioner.Random,  
            Acks = Acks.All,                   
            EnableIdempotence = true,          
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
    }


    public async Task SendOrderCreatedEventAsync(OrderEntity order)
    {
        string json = JsonSerializer.Serialize(order);
        int maxRetries = 5;  
        int retryCount = 0;

        while (retryCount < maxRetries)
        {
            try
            {
                var deliveryResult = await _producer.ProduceAsync(_topic, new Message<string, string>
                {
                    Key = order.Id.ToString(),
                    Value = json
                });

                if (deliveryResult.Status == PersistenceStatus.Persisted)
                {
                    Console.WriteLine($"Message with key {order.Id} successfully delivered to {deliveryResult.TopicPartitionOffset}");
                    break; 
                }
                else
                {
                    Console.WriteLine($"Message with key {order.Id} delivery failed. Status: {deliveryResult.Status}");
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
