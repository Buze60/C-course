using ECommerceBridge.Interfaces;
using ECommerceBridge.Models;
using ECommerceBridge.Repositories;
using ECommerceBridge.Services;

// Repositories
IProductRepository productRepository =
    new ProductRepository();

IOrderRepository orderRepository =
    new OrderRepository();

// Services
IPaymentService paymentService =
    new PaymentService();

IProductService productService =
    new ProductService(productRepository);

IOrderService orderService =
    new OrderService(
        orderRepository,
        productRepository,
        paymentService
    );

// Event subscribers
NotificationService notificationService =
    new NotificationService();

AuditService auditService =
    new AuditService();

orderService.OrderCompleted +=
    notificationService.onOrderCompleted;

orderService.OrderCompleted +=
    auditService.OnOrderCompleted;


// -------------------------
// 1. Create Products
// -------------------------

Product laptop = new Product
{
    Id = 1,
    Name = "Laptop",
    Price = 50000,
    Stock = 10
};

Product headphones = new Product
{
    Id = 2,
    Name = "Headphones",
    Price = 2000,
    Stock = 20
};

productService.AddProduct(laptop);
productService.AddProduct(headphones);


// -------------------------
// 2. Create Customer
// -------------------------

Customer customer = new Customer
{
    Id = 1,
    Name = "Bizu",
    Email = "bizu@example.com"
};


// -------------------------
// 3. Create Order
// -------------------------

OrderItem laptopItem = new OrderItem
{
    Product = laptop,
    Quantity = 2
};

OrderItem headphonesItem = new OrderItem
{
    Product = headphones,
    Quantity = 3
};

Order order = orderService.CreateOrder(
    1001,
    customer,
    new List<OrderItem>
    {
        laptopItem,
        headphonesItem
    }
);

Console.WriteLine("ORDER CREATED");
Console.WriteLine($"Order ID: {order.Id}");
Console.WriteLine($"Customer: {order.Customer.Name}");
Console.WriteLine($"Total: {order.TotalAmount}");
Console.WriteLine($"Status: {order.Status}");


// -------------------------
// 4. Process Payment
// -------------------------

Payment payment =
    await orderService.ProcessPaymentAsync(
        order.Id,
        "Telebirr"
    );

Console.WriteLine();
Console.WriteLine("PAYMENT");
Console.WriteLine($"Amount: {payment.Amount}");
Console.WriteLine($"Method: {payment.Method}");
Console.WriteLine($"Successful: {payment.IsSuccessful}");


// -------------------------
// 5. Final Order
// -------------------------

Console.WriteLine();
Console.WriteLine("FINAL ORDER");
Console.WriteLine($"Status: {order.Status}");
Console.WriteLine($"Total: {order.TotalAmount}");


// -------------------------
// 6. Check Stock
// -------------------------

Product? remainingLaptop =
    productService.GetProduct(1);

Product? remainingHeadphones =
    productService.GetProduct(2);

Console.WriteLine();
Console.WriteLine("REMAINING STOCK");
Console.WriteLine(
    $"Laptop: {remainingLaptop?.Stock}"
);

Console.WriteLine(
    $"Headphones: {remainingHeadphones?.Stock}"
);


// List<Product> products = new List<Product>();
// bool running = true;

// while (running)
// {
//     Console.WriteLine("\n=== Product Menu ===");
//     Console.WriteLine("1. Add Product");
//     Console.WriteLine("2. Create A Customer");
//     Console.WriteLine("3. List Products");
//     Console.WriteLine("5. Use List");
//     Console.WriteLine("6. creat Order");
//     Console.WriteLine("3. Exit");
//     Console.Write("Choose an option: ");

//     string? choice = Console.ReadLine();
//     switch (choice)
//     {
//         case "1":
//             AddProduct();
//             break;
//         case "2":
//             CreateUser();
//             break;
//         case "3":
//             ListProduct();
//             break;
//         case "4":
//             ListUser();
//             break;

//         case "6":
//             CreateOrder();
//             break;
//     }




//     void AddProduct()
//     {
//         var product = new Product();

//         Console.Write("Enter Id: ");
//         product.Id = int.Parse(Console.ReadLine() ?? "0");

//         Console.Write("Enter Name: ");
//         product.Name = Console.ReadLine() ?? string.Empty;

//         Console.Write("Enter Price: ");
//         product.Price = decimal.Parse(Console.ReadLine() ?? "0");

//         Console.Write("Enter Stock: ");
//         product.Stock = int.Parse(Console.ReadLine() ?? "0");

//         // CREATE PRODUCT
//         Product createdProduct = new Product
//         {
//             Id = product.Id,
//             Name = product.Name,
//             Price = product.Price,
//             Stock = product.Stock
//         };
//         productRepository.Add(createdProduct);
//     }

//     void CreateUser()
//     {
//         var user = new Customer();

//         Console.WriteLine("Enter Account Id: ");
//         user.Id = int.Parse(Console.ReadLine() ?? "00");

//         Console.WriteLine("Enter Customer Name: ");
//         user.Name = Console.ReadLine()! ?? "Unknown";

//         Console.WriteLine("Please Enter Email: ");
//         user.Email = Console.ReadLine()! ?? "unknown@gmail.com";

//         Customer customer = new Customer
//         {
//             Id = user.Id,
//             Name = user.Name,
//             Email = user.Email
//         };

//         customerRepository.Add(customer);

//     }

//         void CreateOrder()
//     {
//         var orderItem = new OrderItem();
//         var product = new Product();
//         var order = new Order();
//         var customer = new Customer();

//         Console.WriteLine("CREATE AN ORDER: ");

//         orderItem.Product = product;
//         Console.WriteLine("Enter the Qunatity: ");
//         orderItem.Quantity = int.Parse(Console.ReadLine()!);
        
//          Order orderCreated = new Order
//          {

//              Items = new List<OrderItem> {orderItem},
//              Id = order.Id,
//              Customer = customerRepository.GetById(customer.Id)!,
//              Payment = order.Payment,
//              Status = order.Status
//          };


//     }


//     void ListUser()
//     {
//         Console.WriteLine("=====List of User=====");
//         foreach (Customer customer in customerRepository.GetAll())
//         {
//             Console.WriteLine($"Customer Name: {customer.Name}");
//         }
//     }

//     void ListProduct()
//     {
//         Console.WriteLine("List Of product:");
//         foreach (Product product in productRepository.GetAll())
//         {
//             Console.WriteLine($"Product Name: {product.Name}");
//         }
//     }
// }