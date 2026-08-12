public class Transaction
{
    public Guid TransactionId { get; set; }

    public int AccountNumber { get; set; }

    public DateTime Date { get; set; }

    public TransactionType Type { get; set; }

    public decimal Amount { get; set; }

    public decimal BalanceAfterTransaction { get; set; }
}