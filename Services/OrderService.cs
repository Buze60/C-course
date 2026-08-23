using ECommerceBridge.Interfaces;
using ECommerceBridge.Models;

namespace ECommerceBridge.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository orderRepository;
    private readonly IProductRepository productRepository;
    private readonly IPaymentService paymentService;
    public event Action<Order>? OrderCompleted;

    public OrderService(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IPaymentService paymentService)
    {
        this.orderRepository = orderRepository;
        this.productRepository = productRepository;
        this.paymentService = paymentService;
    }

    public Order CreateOrder(
        int orderId,
        Customer customer,
        List<OrderItem> items)
    {
        if (items.Count == 0)
        {
            throw new InvalidOperationException(
                "Order must contain at least one item."
            );
        }

        foreach (OrderItem item in items)
        {
            Product? product =
                productRepository.GetById(item.Product.Id);

            if (product == null)
            {
                throw new InvalidOperationException(
                    $"Product {item.Product.Id} does not exist."
                );
            }

            if (item.Quantity <= 0)
            {
                throw new ArgumentException(
                    "Quantity must be greater than zero."
                );
            }

            if (item.Quantity > product.Stock)
            {
                throw new InvalidOperationException(
                    $"Not enough stock for {product.Name}."
                );
            }
        }

        Order order = new Order
        {
            Id = orderId,
            Customer = customer,
            Items = items
        };

        orderRepository.Add(order);

        return order;
    }

    public Order? GetOrder(int id)
    {
        return orderRepository.GetById(id);
    }

    public async Task<Payment> ProcessPaymentAsync(
        int orderId,
        string paymentMethod)
    {
        Order? order = orderRepository.GetById(orderId);

        if (order == null)
        {
            throw new InvalidOperationException(
                "Order not found."
            );
        }

        if (order.Payment != null)
        {
            throw new InvalidOperationException(
                "Order has already been paid."
            );
        }

        foreach (OrderItem item in order.Items)
        {
            Product? product = productRepository.GetById(item.Product.Id);

            if (product == null)
            {
                throw new InvalidOperationException(
                    $"Product {item.Product.Id} not found."
                 );
            }

            if (product.Stock < item.Quantity)
            {
                throw new InvalidOperationException(
                    $"Not Enough stock for {product.Name}"
                 );
            }
        }

        Payment payment = await paymentService.ProcessPaymentAsync(
            order,
            paymentMethod
         );

        if (!payment.IsSuccessful)
        {
            return payment;
        }

        foreach (OrderItem item in order.Items)
        {
            Product product = productRepository.GetById(item.Product.Id)!;

            product.Stock -= item.Quantity;

            productRepository.Update(product);
        }

        order.Status = "Completed";

        OrderCompleted?.Invoke(order);

        return payment;

    }
}