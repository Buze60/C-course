public class TransactionReportService
    : ITransactionReportService
{
    public List<Transaction> GetDeposits(
        BankAccount account)
    {
        return account.Transactions
            .Where(t =>
                t.Type == TransactionType.Deposit)
            .ToList();
    }

    public List<Transaction> GetWithdrawals(
        BankAccount account)
    {
        return account.Transactions
            .Where(t =>
                t.Type == TransactionType.Withdrawal)
            .ToList();
    }

    public List<Transaction> GetTransactionsAbove(
        BankAccount account,
        decimal amount)
    {
        return account.Transactions
            .Where(t => t.Amount > amount)
            .ToList();
    }

    public decimal GetTotalDeposits(
        BankAccount account)
    {
        return account.Transactions
            .Where(t =>
                t.Type == TransactionType.Deposit)
            .Sum(t => t.Amount);
    }

    public decimal GetTotalWithdrawals(
        BankAccount account)
    {
        return account.Transactions
            .Where(t =>
                t.Type == TransactionType.Withdrawal)
            .Sum(t => t.Amount);
    }

    public List<Transaction> GetRecentTransactions(
        BankAccount account)
    {
        return account.Transactions
            .OrderByDescending(t => t.Date)
            .ToList();
    }

    public List<Transaction> Search(
        BankAccount account,
        TransactionFilter filter)
    {
        IEnumerable<Transaction> query =
            account.Transactions;

        if (filter.Type.HasValue)
        {
            query = query.Where(t =>
                t.Type == filter.Type.Value);
        }

        if (filter.MinimumAmount.HasValue)
        {
            query = query.Where(t =>
                t.Amount >=
                filter.MinimumAmount.Value);
        }

        if (filter.MaximumAmount.HasValue)
        {
            query = query.Where(t =>
                t.Amount <=
                filter.MaximumAmount.Value);
        }

        if (filter.FromDate.HasValue)
        {
            query = query.Where(t =>
                t.Date >=
                filter.FromDate.Value);
        }

        if (filter.ToDate.HasValue)
        {
            query = query.Where(t =>
                t.Date <=
                filter.ToDate.Value);
        }

        return query
            .OrderByDescending(t => t.Date)
            .ToList();
    }
}