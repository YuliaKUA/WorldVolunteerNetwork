using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WorldVolunteerNetwork.Infrastructure.Kafka
{
    public class KafkaMessageConsumer : BackgroundService
    {
        private readonly ILogger<KafkaMessageConsumer> _logger;

        public KafkaMessageConsumer(ILogger<KafkaMessageConsumer> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            var config = new ConsumerConfig
            {
                BootstrapServers = "172.18.0.9:9092",
                GroupId = "group",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                AllowAutoCreateTopics = true,
            };

            try
            {
                using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
                {
                    consumer.Subscribe("test-topic");

                    while (!stoppingToken.IsCancellationRequested)
                    {
                        var kafkaMessage = consumer.Consume(stoppingToken);
                        if (kafkaMessage is null)
                        {
                            _logger.LogInformation("Message is null");
                        }

                        //processing (like send email)

                        _logger.LogInformation("Message consumed: {message}", kafkaMessage.Message.Value);
                    }

                    consumer.Close();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Error while kafka message consuming: {message}", ex.Message);
            }

            await Task.CompletedTask;
        }
    }
}
