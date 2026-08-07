using System.ComponentModel.Design;

public class BankService
{

    private List<BankAccount> service = new();
   

    //CREATE BANK ACCOUNT
    public void AddAccount(BankAccount account)
    {
        service.Add(account);
    }


    // DEPOSIT BALANCE LOGIC
    public void Deposit(decimal amount, int accountNumber)
    {
        BankAccount? account = service.FirstOrDefault(a => a.AccountNumber == accountNumber);
        // BankAccount account = new ();
        account?.Balance += amount;
      

    }

    // WITHDRAW MONEY
    public void Withdraw(decimal amount, int accountNumber)
    {
        BankAccount? account = service.FirstOrDefault(a => a.AccountNumber == accountNumber);
        if (account == null && amount! <= 0)
        {
            account?.Balance -= amount;
            
        }
    }

    //List User of the bank
    public List<BankAccount> UserList()
    {
        return service;
        
    }






}