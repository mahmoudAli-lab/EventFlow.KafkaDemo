using System.Threading.Tasks;
using EventFlow.KafkaDemo;
using Xunit;

namespace UnitTests
{
    public class SampleUnitTests
    {
        [Fact]
        public async Task UserCreation_CreatesUserWithEmail()
        {
            var svc = new UserService();
            var user = await svc.CreateUserAsync("alice@example.com");
            Assert.Equal("alice@example.com", user.Email);
        }

        [Fact]
        public async Task OrderPayment_FailsWhenAmountTooLarge()
        {
            var orderSvc = new OrderService();
            var paymentSvc = new PaymentService();

            var order = await orderSvc.CreateOrderAsync(System.Guid.NewGuid(), 2000m);
            var payment = await paymentSvc.ProcessPaymentAsync(order.OrderId, order.Amount);

            Assert.False(payment.Success);
        }
    }
}
