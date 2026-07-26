using System;

public class SmsService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"📱 SMS: {message}");
    }

    public void Recived(string id)
    {
        Console.WriteLine($"Hello from .NET");
    }
}