public class TransactionService : ITransactionService
{

    private readonly IBankRepository repository;
    public event EventHandler<TransactionCompletedEventArgs>? TransactionCompleted;


    public TransactionService(IBankRepository repository)
    {
        this.repository = repository;
    }



    public void AddTransaction(BankAccount account, TransactionType type, decimal amount)
    {
        Transaction transaction = new Transaction
        {
            TransactionId = Guid.NewGuid(),
            AccountNumber = account.AccountNumber,
            Date = DateTime.Now,
            Type = type,
            Amount = amount,
            BalanceAfterTransaction = account.Balance
        };
        account.Transactions.Add(transaction);

        TransactionCompleted?.Invoke(this,new TransactionCompletedEventArgs(
            account:account,
            transaction:transaction
        ));

    }

    // GET ALL TRANSACTION DATA
    public List<Transaction> GetTransactions(BankAccount account)
    {
        return account.Transactions;

    }


    // FILTER Withdrwal transaction 
    public List<Transaction> GetWithdrawals(BankAccount account)
    {
        return account.Transactions
            .Where(t => t.Type == TransactionType.Withdrawal)
            .ToList();
    }

    // FILTER DEPOSIT
    public List<Transaction> GetDeposit(BankAccount account)
    {
        return account.Transactions
            .Where(t => t.Type == TransactionType.Deposit)
            .ToList();
    }

}