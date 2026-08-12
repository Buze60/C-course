public class BankService
{
    private readonly IBankRepository repository;
    private readonly ITransactionService transactionService;
    public event EventHandler<MoneyWithdrawnEventArgs>? MoneyWithdrawn;
    public event EventHandler<MoneyDepositedEventArgs>? MoneyDeposited;


    public BankService(IBankRepository repository, ITransactionService transactionService)
    {
        this.repository = repository;
        this.transactionService = transactionService;
    }
    //CREATE NEW ACCOUNT 
    public void CreateAccount(int accountNumber, string ownerName, decimal initialBalance)
    {
        BankAccount account = new BankAccount
        {
            AccountNumber = accountNumber,
            OwnerName = ownerName,
            Balance = initialBalance
        };

        repository.Add(account);
    }

    // GET THE CREATED ACCOUNT

    public BankAccount? GetAccount(int accountNumber)
    {
        return repository.GetByAccountNumber(accountNumber);
    }


    // DEPOSIT METHOD
    public void Deposit(int accountNumber, decimal amount)
    {
        BankAccount? account =
            repository.GetByAccountNumber(accountNumber);

        if (account == null)
        {
            throw new InvalidOperationException(
          $"Account {accountNumber} was not found."
      );
        }

        if (amount <= 0)
        {
            throw new ArgumentException("Deposit amount must be greater than zero.");
        }

        account.Balance += amount;

        transactionService.AddTransaction(

            account,
            TransactionType.Deposit,
            amount
        );

        MoneyDeposited?.Invoke(
            this,
            new MoneyDepositedEventArgs(
                account,
                amount,
                account.Balance
            )
        );

    }

    // WITHDRAW METHOD
    public void Withdraw(int accountNumber, decimal amount)
    {
        BankAccount? account =
            repository.GetByAccountNumber(accountNumber);

        if (account == null)
        {
            throw new InvalidOperationException("Account not found.");
        }

        if (amount <= 0)
        {
            throw new ArgumentException("Withdrawal amount must be greater than zero.");
        }

        if (account.Balance < amount)
        {
            throw new InvalidOperationException(
        "Insufficient balance."
    );

        }

        account.Balance -= amount;

        transactionService.AddTransaction(
            account,
            TransactionType.Withdrawal,
            amount
        );

        MoneyWithdrawn?.Invoke(
            this,
            new MoneyWithdrawnEventArgs(
                account,
                amount,
                account.Balance
            )
        );
    }
}