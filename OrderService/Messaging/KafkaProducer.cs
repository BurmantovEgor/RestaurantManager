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
            BootstrapServers = "kafka:9092"
        };
        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task SendOrderCreatedEventAsync(OrderEntity order)
    {
        string json = JsonSerializer.Serialize(order);
        Console.WriteLine(order.ToString());
        await _producer.ProduceAsync(_topic, new Message<string, string> { Key = order.Id.ToString(), Value = json });
    }
}
