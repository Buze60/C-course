using ECommerceBridge.Models;
namespace ECommerceBridge.Interfaces;

public interface IProductService
{
    void AddProduct(Product product);
    Product?GetProduct(int id);
    IEnumerable<Product>GetProducts();
}