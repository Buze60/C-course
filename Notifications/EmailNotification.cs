public class EmailNotification
{
    public void SendEmail(
        BankAccount account,
        decimal amount)
    {
        Console.WriteLine(
            $"EMAIL: {account.OwnerName}, " +
            $"{amount} was withdrawn from your account."
        );
    }
}