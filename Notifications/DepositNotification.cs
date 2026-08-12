public class DepositNotification
{
    public void SendNotification(
        object? sender,
        MoneyDepositedEventArgs e)
    {
        Console.WriteLine(
            $"DEPOSIT: {e.Account.OwnerName} " +
            $"deposited {e.Amount}. " +
            $"New balance: {e.NewBalance}"
        );
    }
}