public class WhatsAppService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"💬 WhatsApp: {message}");
    }
}