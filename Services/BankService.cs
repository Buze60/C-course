public class BankService
{
    private readonly IBankRepository repository;
    public event Action<BankAccount, decimal>? MoneyWithdrawn;

    public BankService(IBankRepository repository)
    {
        this.repository = repository;
    }
    //CREATE NEW ACCOUNT 
    public void CreateAccount(
    int accountNumber,
    string ownerName,
    decimal initialBalance)
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
            Console.WriteLine("Account not found.");
            return;
        }

        account.Balance += amount;

        Transaction transaction = new Transaction
        {
            Date = DateTime.Now,
            Type = "Deposit",
            Amount = amount
        };

        account.Transactions.Add(transaction);
    }

    // WITHDRAW METHOD
    public void Withdraw(int accountNumber, decimal amount)
    {
        BankAccount? account =
            repository.GetByAccountNumber(accountNumber);

        if (account == null)
        {
            Console.WriteLine("Account not found.");
            return;
        }

        if (account.Balance < amount)
        {
            Console.WriteLine("Insufficient balance.");
            return;
        }

        account.Balance -= amount;

        Transaction transaction = new Transaction
        {
            Date = DateTime.Now,
            Type = "Withdrawal",
            Amount = amount
        };

        account.Transactions.Add(transaction);

        MoneyWithdrawn?.Invoke(account, amount);
    }


}