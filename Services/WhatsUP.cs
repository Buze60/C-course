namespace ECommerceBridge.Services;

public class WhatsUP
{
    public void onCompletedNotification(Order order)
    {
        Console.WriteLine($"Custumer Name: {order.Customer.Name}\nEmail: {order.Customer.Email}\ndelivered: {order.Id}");
    }
}