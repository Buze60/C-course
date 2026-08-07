public interface IBankRepository
{
    void Add(BankAccount account);

    BankAccount? GetByAccountNumber(int accountNumber);

    List<BankAccount> GetAll();
}