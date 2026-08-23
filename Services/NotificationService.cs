using ECommerceBridge.Models;
namespace ECommerceBridge.Services;

public class NotificationService
{
    public void onOrderCompleted(Order order)
    {
        Console.WriteLine($"[NOTIFICATION] Order #{order.Id} completed for {order.Customer.Name}");
    }
}