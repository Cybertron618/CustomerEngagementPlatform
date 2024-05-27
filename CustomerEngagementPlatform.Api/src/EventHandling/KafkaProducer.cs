using Confluent.Kafka;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomerEngagementPlatform.Api.src.EventHandling
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly string _topic;
        private readonly IProducer<Null, string> _producer;

        public KafkaProducer(string bootstrapServers, string topic)
        {
            _topic = topic;
            var config = new ProducerConfig { BootstrapServers = bootstrapServers };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task ProduceAsync<T>(T message)
        {
            var jsonMessage = JsonSerializer.Serialize(message);
            await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = jsonMessage });
        }
    }
}
