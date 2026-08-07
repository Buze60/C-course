public class BankRepository : IBankRepository
{
    private readonly List<BankAccount> accounts = new();

    public void Add(BankAccount account)
    {
        accounts.Add(account);
    }

    public BankAccount? GetByAccountNumber(int accountNumber)
    {
        return accounts.FirstOrDefault(
            account => account.AccountNumber == accountNumber
        );
    }

    public List<BankAccount> GetAll()
    {
        return accounts;
    }
}