public class SmsNotification
{
    public void SendSmsNotification(
    object? sender,
    MoneyWithdrawnEventArgs e)
    {
        Console.WriteLine(
            $"SMS:💬 {e.Account.OwnerName}\nAmoutn:{e.Amount} was withdrawn. \nRemaining balance: {e.RemainingBalance}"
        );
    }

}