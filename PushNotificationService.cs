using System;

public class PushNotificationService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"🔔 Push: {message}");
    }

    public void Recived(string id)
    {
        Console.WriteLine($"Hello from .NET");
    }
}