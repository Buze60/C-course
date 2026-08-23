using ECommerceBridge.Models;

public class Order
{
    public int Id { get; set; }
    public Customer Customer { get; set; } = null!;
    public List<OrderItem> Items { get; set; } = new();
    public Payment? Payment { get; set; }
    public string Status {get;set;} = "Pending";
    public decimal TotalAmount => Items.Sum(item => item.Totalprice);
}