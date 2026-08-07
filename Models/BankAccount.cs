public class BankAccount
{
    public int AccountNumber { get; set; }

    public string OwnerName { get; set; } = "";

    public decimal Balance { get; set; }

    public List<Transaction> Transactions {get;set;} = new();
}
