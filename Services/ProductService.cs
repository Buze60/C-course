using ECommerceBridge.Interfaces;
using ECommerceBridge.Models;
namespace ECommerceBridge.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository productRepository;

    public ProductService(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }


    public void AddProduct(Product product)
    {
        if (product.Price <= 0)
        {
            throw new ArgumentException(
                "Product price must be greater than zero."
            );
        }

        if (product.Stock < 0)
        {
            throw new ArgumentException("Stock cannot be negative.");
        }

        productRepository.Add(product);
    }

    public Product? GetProduct(int id)
    {
        return productRepository.GetById(id);
    }

    public IEnumerable<Product> GetProducts()
    {
        return productRepository.GetAll();
    }
}