public class TransactionCompletedEventArgs: EventArgs
{
    public Transaction Transaction { get; }

    public BankAccount Account { get; }

    public TransactionCompletedEventArgs(
        BankAccount account,
        Transaction transaction)
    {
        Account = account;
        Transaction = transaction;
    }
}