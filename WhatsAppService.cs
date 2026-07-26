public class WhatsAppService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"💬 WhatsApp: {message}");
    }

    public void Recived(string id)
    {
        Console.WriteLine($"Recived: {id}");
    }
}