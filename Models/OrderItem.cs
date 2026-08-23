using ECommerceBridge.Models;

public class OrderItem
{
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Totalprice => Product.Price * Quantity;
}