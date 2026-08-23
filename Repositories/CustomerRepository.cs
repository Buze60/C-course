using ECommerceBridge.Interfaces;

public class CustomerRepository : ICustomerRepository
{
    private readonly List<Customer> customers = new();
    public void Add(Customer customer)
    {
        customers.Add(customer);
    }

    public Customer? GetById(int id)
    {
        return customers.FirstOrDefault(c => c.Id == id);
    }

    public IEnumerable<Customer> GetAll()
    {
        return customers;
    }
}