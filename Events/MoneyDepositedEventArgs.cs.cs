public class MoneyDepositedEventArgs : EventArgs
{
    public BankAccount Account { get; }

    public decimal Amount { get; }

    public decimal NewBalance { get; }

    public MoneyDepositedEventArgs(
        BankAccount account,
        decimal amount,
        decimal newBalance)
    {
        Account = account;
        Amount = amount;
        NewBalance = newBalance;
    }
}