using ECommerceBridge.Interfaces;
using ECommerceBridge.Models;

namespace ECommerceBridge.Services;

public class PaymentService : IPaymentService
{
    private int nextPaymentId = 1;

    public async Task<Payment> ProcessPaymentAsync(
        Order order,
        string method)
    {
        if (order.TotalAmount <= 0)
        {
            throw new InvalidOperationException(
                "Order amount must be greater than zero.");
        }

        // Simulate communication with a payment provider
        await Task.Delay(1000);

        Payment payment = new Payment
        {
            Id = nextPaymentId++,
            Amount = order.TotalAmount,
            Method = method,
            IsSuccessful = true
        };

        order.Payment = payment;

        return payment;
    }
}