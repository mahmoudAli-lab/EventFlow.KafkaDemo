using System.Threading.Tasks;
using EventFlow.KafkaDemo;
using Xunit;

namespace E2ETests
{
    public class HappyPathE2ETests
    {
        [Fact]
        public async Task FullFlow_HappyPath()
        {
            var userSvc = new UserService();
            var orderSvc = new OrderService();
            var paymentSvc = new PaymentService();
            var invSvc = new InventoryService();

            var user = await userSvc.CreateUserAsync("bob@example.com");
            var order = await orderSvc.CreateOrderAsync(user.UserId, 99m);
            var payment = await paymentSvc.ProcessPaymentAsync(order.OrderId, order.Amount);
            var inv = await invSvc.ReserveAsync(order.OrderId, 1);

            Assert.True(payment.Success);
            Assert.True(inv.Reserved);
        }
    }
}
