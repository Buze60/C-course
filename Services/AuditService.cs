public class AuditService
{
    public void OnTransactionCompleted(
        object? sender,
        TransactionCompletedEventArgs e)
    {
        Console.WriteLine(
            $"AUDIT: " +
            $"Account {e.Account.AccountNumber} " +
            $"performed {e.Transaction.Type} " +
            $"of {e.Transaction.Amount}."
        );
    }
}