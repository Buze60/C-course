using ECommerceBridge.Interfaces;
using ECommerceBridge.Models;

namespace ECommerceBridge.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly List<Order> orders = new();

    public void Add(Order order)
    {
        orders.Add(order);
    }

    public Order? GetById(int id)
    {
        return orders.FirstOrDefault(
            o => o.Id == id
        );
    }

    public IEnumerable<Order> GetAll()
    {
        return orders;
    }
}