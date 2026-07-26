using System;

public class SmsService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"📱 SMS: {message}");
    }
}