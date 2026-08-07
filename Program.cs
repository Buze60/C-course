class Program
{
    static void Main()
    {
        IBankRepository repository = new BankRepository();

        BankService service = new BankService(repository);
        SmsNotification sms = new SmsNotification();
        EmailNotification email = new EmailNotification();

        service.MoneyWithdrawn += sms.SendSms;
        service.MoneyWithdrawn += email.SendEmail;
        service.MoneyWithdrawn -= sms.SendSms;
        service.CreateAccount(
            1001,
            "Bizu",
            5000
        );

        service.Deposit(
            1001,
            1000
        );

        service.Withdraw(
            1001,
            500
        );

        BankAccount? account =
            service.GetAccount(1001);

        Console.WriteLine(
            $"Owner: {account?.OwnerName}"
        );

        Console.WriteLine(
            $"Balance: {account?.Balance}"
        );

        Console.WriteLine(
            $"Transactions: {account?.Transactions.Count}"
        );

        Console.WriteLine("\nTransaction History:");

        foreach (Transaction transaction
                 in account!.Transactions)
        {
            Console.WriteLine(
                $"{transaction.Date} | " +
                $"{transaction.Type} | " +
                $"{transaction.Amount}"
            );
        }

        service.Withdraw(1001, 1000);
    }
}