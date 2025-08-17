using System.Diagnostics;
using System.Threading.Tasks;
using EventFlow.KafkaDemo;
using Xunit;

namespace PerformanceTests
{
    public class ThroughputTests
    {
        [Fact]
        public async Task PaymentService_Throughput()
        {
            var svc = new PaymentService();
            var sw = Stopwatch.StartNew();
            var count = 1000;
            for (int i = 0; i < count; i++)
            {
                await svc.ProcessPaymentAsync(System.Guid.NewGuid(), 10m);
            }
            sw.Stop();
            var throughput = count / sw.Elapsed.TotalSeconds;
            Assert.True(throughput > 100, $"Throughput too low: {throughput}");
        }
    }
}
