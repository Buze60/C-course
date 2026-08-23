using ECommerceBridge.Models;
namespace ECommerceBridge.Interfaces;

public interface ICustomerRepository
{
    void Add(Customer customer);
    Customer?GetById(int id);
    IEnumerable<Customer>GetAll();
}