public interface ITransactionReportService
{
    List<Transaction> GetDeposits(BankAccount account);

    List<Transaction> GetWithdrawals(BankAccount account);

    List<Transaction> GetTransactionsAbove(BankAccount account, decimal amount);

    decimal GetTotalDeposits(BankAccount account);

    decimal GetTotalWithdrawals(BankAccount account);

    List<Transaction> Search(BankAccount account, TransactionFilter filter);
}