public class SmsNotification
{
    public void SendSms(BankAccount account,decimal amount)
    {
        Console.WriteLine($"SMS: {account.OwnerName}, " + $"{amount} was withdrawn from your account.");
    }
}