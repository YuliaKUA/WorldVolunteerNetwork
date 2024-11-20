using Confluent.Kafka;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Error = WorldVolunteerNetwork.Domain.Common.Error;

namespace WorldVolunteerNetwork.Infrastructure.Kafka
{
    public class KafkaMessageProducer
    {
        private readonly ILogger<KafkaMessageProducer> _logger;
        private readonly IProducer<Null, string> _producer;

        public KafkaMessageProducer(ILogger<KafkaMessageProducer> logger)
        {
            _producer = CreateProducer();
            _logger = logger;
        }

        private IProducer<Null, string> CreateProducer()
        {
            var config = new ProducerConfig()
            {
                BootstrapServers = "172.18.0.9:9092",
                AllowAutoCreateTopics = true,
                ClientId = "WorldVolunteerNetwork"
            };

            return new ProducerBuilder<Null, string>(config).Build();
        }
        public async Task<Result<string, Error>> Publish(string topic, string message)
        {
            var kafkaMessage = new Message<Null, string>()
            {
                Value = message
            };

            var deliveryResult = await _producer.ProduceAsync(topic, kafkaMessage);
            if (deliveryResult.Status == PersistenceStatus.NotPersisted)
            {
                _logger.LogError("Message not persisted: {message}", kafkaMessage.Value);
                return Errors.Kafka.PersistFail();
            }

            _logger.LogInformation("Message persisted: {message}", kafkaMessage.Value);

            return Result.Success<string, Error>(kafkaMessage.Value);
        }
    }
}
