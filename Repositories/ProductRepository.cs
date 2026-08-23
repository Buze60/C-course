using ECommerceBridge.Interfaces;
using ECommerceBridge.Models;
public class ProductRepository : IProductRepository
{
    private readonly List<Product> products = new();
    public void Add(Product product)
    {
        products.Add(product);
    }

    public Product? GetById(int id)
    {
        return products.FirstOrDefault(p => p.Id == id);
    }

    public IEnumerable<Product> GetAll()
    {
        return products;
    }

    public void Update(Product product)
    {
        Product? existingProduct = GetById(product.Id);
        if (existingProduct==null)
        {
            throw new InvalidOperationException(
                "Product not found. "
             );
        }

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Stock = product.Stock;
    }
}