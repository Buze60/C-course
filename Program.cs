class Program
{
    static void Main()
    {
        BankService service = new();

        BankAccount account = new()
        {
            AccountNumber = 10002,
            Owner = "Bizu",
            Balance = 5000
        };
        // CREATE NEW ACCOUNT
        service.AddAccount(account);
        // DEPOSIT TO EXISTING ACCOUNT
        service.Deposit(1001, 1000);
        // Withdarw
        service.Withdraw(500, 10002);

     

        // LIST OF THE BANK USERS
        foreach (var user in service.UserList())
        {
            Console.WriteLine(user.Owner);
        }




    }
}