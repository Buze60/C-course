public interface ITransactionService
{
    event EventHandler<TransactionCompletedEventArgs>?TransactionCompleted;
    void AddTransaction(BankAccount account, TransactionType type, decimal amount);
    List<Transaction> GetTransactions(BankAccount account);
    List<Transaction> GetWithdrawals(BankAccount account);
    List<Transaction> GetDeposit(BankAccount account);
}