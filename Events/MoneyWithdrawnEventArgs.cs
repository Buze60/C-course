public class MoneyWithdrawnEventArgs : EventArgs
{
    public BankAccount Account { get; }

    public decimal Amount { get; }

    public decimal RemainingBalance { get; }

    public MoneyWithdrawnEventArgs(
        BankAccount account,
        decimal amount,
        decimal remainingBalance
        )
    {
        Account = account;
        Amount = amount;
        RemainingBalance = remainingBalance;
      
    }
}