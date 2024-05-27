using Confluent.Kafka;
using CustomerEngagementPlatform.Api.src.EventHandling;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerEngagementPlatform.Api.src.EventHandling
{
    public class KafkaConsumer : IKafkaConsumer
    {
        private readonly string _topic;
        private readonly IConsumer<Null, string> _consumer;

        public KafkaConsumer(string bootstrapServers, string topic, string groupId)
        {
            _topic = topic;
            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _consumer = new ConsumerBuilder<Null, string>(config).Build();
        }

        public void Consume<T>(Func<T, Task> handleMessage, CancellationToken cancellationToken)
        {
            _consumer.Subscribe(_topic);

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var consumeResult = _consumer.Consume(cancellationToken);

                    if (consumeResult?.Message?.Value != null)
                    {
                        var message = JsonSerializer.Deserialize<T>(consumeResult.Message.Value);

                        if (message != null)
                        {
                            // Asynchronous handling of the message
                            _ = Task.Run(async () =>
                            {
                                await handleMessage(message).ConfigureAwait(false);
                            }, cancellationToken); // Forwarding cancellationToken to Task.Run
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                _consumer.Close();
            }
        }
    }
}
