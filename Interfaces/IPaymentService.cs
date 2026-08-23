using ECommerceBridge.Models;

namespace ECommerceBridge.Interfaces;

public interface IPaymentService
{
    Task<Payment> ProcessPaymentAsync(Order order, string method);
}