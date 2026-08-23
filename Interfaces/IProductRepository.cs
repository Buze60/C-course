using ECommerceBridge.Models;

namespace ECommerceBridge.Interfaces;

public interface IProductRepository
{
    void Add(Product product);
    Product? GetById(int id);
    IEnumerable<Product> GetAll();
    void Update(Product product);
}


