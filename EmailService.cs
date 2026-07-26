using System;

public class EmailService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"📧 Email: {message}");
    }
}