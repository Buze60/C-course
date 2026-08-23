namespace ECommerceBridge.Models;

public class Payment
{
    public int Id { get; set; }

    public decimal Amount { get; set; }

    public string Method { get; set; } = string.Empty;

    public bool IsSuccessful { get; set; }
}