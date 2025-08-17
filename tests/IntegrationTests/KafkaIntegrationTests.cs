using System;
using System.Threading.Tasks;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Xunit;

namespace IntegrationTests
{
    public class KafkaIntegrationTests : IAsyncLifetime
    {
        private TestcontainersContainer _kafkaContainer = null!;

        public async Task InitializeAsync()
        {
            _kafkaContainer = new TestcontainersBuilder<TestcontainersContainer>()
                .WithImage("confluentinc/cp-kafka:7.4.0")
                .WithName("test-kafka")
                .WithEnvironment("KAFKA_BROKER_ID", "1")
                .WithEnvironment("KAFKA_ZOOKEEPER_CONNECT", "zookeeper:2181")
                .WithEnvironment("KAFKA_LISTENER_SECURITY_PROTOCOL_MAP", "PLAINTEXT:PLAINTEXT,PLAINTEXT_HOST:PLAINTEXT")
                .WithEnvironment("KAFKA_ADVERTISED_LISTENERS", "PLAINTEXT://localhost:9092")
                .WithEnvironment("KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR", "1")
                .WithPortBinding(9092, 9092)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged("started (kafka.server.KafkaServer)"))
                .Build();

            await _kafkaContainer.StartAsync();
        }

        [Fact]
        public async Task KafkaContainerStarts()
        {
            Assert.True(_kafkaContainer != null && _kafkaContainer.IsRunning);
        }

        public async Task DisposeAsync()
        {
            if (_kafkaContainer != null)
            {
                await _kafkaContainer.StopAsync();
                _kafkaContainer.Dispose();
            }
        }
    }
}
