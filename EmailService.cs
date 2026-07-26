using System;

public class EmailService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"📧 Email: {message}");
    }
    public void Recived(string id)
    {
        Console.WriteLine($"Hello from .NET");
    }
}