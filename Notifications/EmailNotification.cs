public class EmailNotification
{
    public void SendEmailWithdraw(
        object? sender,
        MoneyWithdrawnEventArgs e)
    {
        Console.WriteLine(
            $"Email:📧 {e.Account.OwnerName}\nAmoutn:{e.Amount} was withdrawn. \nRemaining balance: {e.RemainingBalance}"
        );
    }


}