using ECommerceBridge.Models;
public interface IOrderRepository
{
    void Add(Order order);
    Order? GetById(int order);
    IEnumerable<Order> GetAll();
}