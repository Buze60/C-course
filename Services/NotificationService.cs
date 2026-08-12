public class NotificationService
{
    public void OnTransactionCompleted(
        object? sender,
        TransactionCompletedEventArgs e)
    {
        Console.WriteLine(
            $"Notification: " +
            $"{e.Transaction.Type} of " +
            $"{e.Transaction.Amount} completed " +
            $"for {e.Account.OwnerName}."
        );
    }
}