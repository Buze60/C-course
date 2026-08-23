using ECommerceBridge.Models;

namespace ECommerceBridge.Services;

public class AuditService
{
    public void OnOrderCompleted(Order order)
    {
        Console.WriteLine(
            $"[AUDIT] Order #{order.Id} completed. Amount: {order.TotalAmount}"
        );
    }
}