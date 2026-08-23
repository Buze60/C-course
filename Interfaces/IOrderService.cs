using ECommerceBridge.Models;

namespace ECommerceBridge.Interfaces;

public interface IOrderService
{
    event Action<Order>? OrderCompleted;
    Order CreateOrder(
        int orderId,
        Customer customer,
        List<OrderItem> items
    );

    Order? GetOrder(int id);

   Task <Payment> ProcessPaymentAsync(
        int orderId,
        string paymentMethod
    );
}