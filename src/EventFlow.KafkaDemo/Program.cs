using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventFlow.KafkaDemo
{
    // Simple event models
    public record UserCreated(Guid UserId, string Email);
    public record OrderCreated(Guid OrderId, Guid UserId, decimal Amount);
    public record PaymentProcessed(Guid OrderId, bool Success);
    public record InventoryReserved(Guid OrderId, bool Reserved);

    // Very small in-memory services used by unit tests
    public class UserService
    {
        private readonly List<UserCreated> _users = new();
        public Task<UserCreated> CreateUserAsync(string email)
        {
            var user = new UserCreated(Guid.NewGuid(), email);
            _users.Add(user);
            return Task.FromResult(user);
        }
    }

    public class OrderService
    {
        public Task<OrderCreated> CreateOrderAsync(Guid userId, decimal amount)
        {
            var order = new OrderCreated(Guid.NewGuid(), userId, amount);
            return Task.FromResult(order);
        }
    }

    public class PaymentService
    {
        public Task<PaymentProcessed> ProcessPaymentAsync(Guid orderId, decimal amount)
        {
            // Accept payments < 1000 for happy path
            var success = amount < 1000m;
            return Task.FromResult(new PaymentProcessed(orderId, success));
        }
    }

    public class InventoryService
    {
        public Task<InventoryReserved> ReserveAsync(Guid orderId, int qty)
        {
            // Always reserve for demo
            return Task.FromResult(new InventoryReserved(orderId, true));
        }
    }

    public class Program
    {
        public static Task Main(string[] args)
        {
            Console.WriteLine("EventFlow.KafkaDemo sample service — not a real host.");
            return Task.CompletedTask;
        }
    }
}
