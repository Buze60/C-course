using System;

public class PushNotificationService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"🔔 Push: {message}");
    }
}