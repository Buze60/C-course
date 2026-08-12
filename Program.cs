class Program
{
    static void Main()
    {
        IBankRepository repository = new BankRepository();

        ITransactionService transactionService = new TransactionService(repository);

        BankService service = new BankService(repository, transactionService);

        ITransactionReportService reportService = new TransactionReportService();
        NotificationService notificationService = new NotificationService();
        AuditService auditService = new AuditService();
        transactionService.TransactionCompleted += auditService.OnTransactionCompleted;

        transactionService.TransactionCompleted += notificationService.OnTransactionCompleted;




        SmsNotification sms = new SmsNotification();

        EmailNotification email = new EmailNotification();

        DepositNotification depositNotification = new DepositNotification();

        service.MoneyWithdrawn += sms.SendSmsNotification;

        service.MoneyWithdrawn += email.SendEmailWithdraw;

        service.MoneyDeposited += depositNotification.SendNotification;


        service.CreateAccount(
            1001,
            "Bizu",
            2000
        );
        Console.WriteLine();



        try
        {
            service.Withdraw(1001, 10000);
        }
        catch (InvalidOperationException ex)
        {

            Console.WriteLine($"Error: {ex.Message}");
        }
        ;




        BankAccount? account = service.GetAccount(1001);
        decimal totalDeposits =
            reportService.GetTotalDeposits(account!);

        decimal totalWithdrawals =
            reportService.GetTotalWithdrawals(account!);

        Console.WriteLine($"Total Deposits: {totalDeposits}");

        Console.WriteLine($"Total Withdrawals: {totalWithdrawals}");


        Console.WriteLine();

        Console.WriteLine($"Owner: {account?.OwnerName}");

        Console.WriteLine($"Balance: {account?.Balance}");

        Console.WriteLine(
            $"Transactions: " +
            $"{account?.Transactions.Count}"
        );

        List<Transaction> transactions = transactionService.GetTransactions(account!);


        foreach (Transaction transaction in account!.Transactions)
        {
            Console.WriteLine(
         $"{transaction.TransactionId} | " +
         $"{transaction.Date} | " +
         $"{transaction.Type} | " +
         $"{transaction.Amount} | " +
         $"{transaction.BalanceAfterTransaction}");
        }


        Console.WriteLine();
        Console.WriteLine("===== TRANSACTION REPORT =====");

        Console.WriteLine(
            $"Account: {account!.AccountNumber}"
        );

        Console.WriteLine(
            $"Owner: {account.OwnerName}"
        );

        Console.WriteLine(
            $"Current Balance: {account.Balance}"
        );

        Console.WriteLine(
            $"Total Deposits: " +
            $"{reportService.GetTotalDeposits(account)}"
        );

        Console.WriteLine(
            $"Total Withdrawals: " +
            $"{reportService.GetTotalWithdrawals(account)}"
        );

        Console.WriteLine(
            $"Transaction Count: " +
            $"{account.Transactions.Count}"
        );



        Console.WriteLine();
        Console.WriteLine();

        TransactionFilter filter = new TransactionFilter
        {
            Type = TransactionType.Withdrawal,
            MinimumAmount = 500,
            MaximumAmount = 5000
        };

        List<Transaction> results =
            reportService.Search(
                account!,
                filter
            );

        Console.WriteLine(
            "\n===== SEARCH RESULTS ====="
        );

        foreach (Transaction transaction in results)
        {
            Console.WriteLine(
                $"{transaction.Date} | " +
                $"{transaction.Type} | " +
                $"{transaction.Amount} | " +
                $"{transaction.BalanceAfterTransaction}"
            );
        }
    }
}