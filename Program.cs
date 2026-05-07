using System.Security.Cryptography.X509Certificates;

namespace ShopNest_E_CommerceSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello to ShopNest E-Commerce System");
        }
    }
    class Hierarchy
    {

    }
    abstract class Product
    {
        private static int nextProductID = 100;
        private static int totalProductsCreated;
        protected string name;
        protected double price;

        //Properties 

        public int ProductID { get; }
        public string Name
        {
            get { return name; }
        }
        public double Price
        {
            get { return price; }
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Price must be greater than 0");
                }
                else
                {
                    price= value;
                }
            }





        }
        public Product(string name, double price)
        {
            this.name = name;
            Price = price;
            ProductID = nextProductID++;
            totalProductsCreated++;
        }
        public static int GetTotalProductsCreated()
        {
            return totalProductsCreated;
        }
        public abstract void DisplayInfo();   

        public virtual double CalculateTotalCost()
        {
            return price;
        }

    }

    class PhysicalProduct : Product 
    {
        private double weightKg;
        private double shippingCostPerKg;
        //properties
        public double WeightKg
        {
            get { return weightKg; }
        }
        public PhysicalProduct(string name, double price, double weightKg, double shippingCostPerKg) :base(name,price)
        {
            this.weightKg= weightKg;
            this.shippingCostPerKg= shippingCostPerKg;

        }
        public override void DisplayInfo()
        {
            Console.WriteLine("=============Physical Product===============");
            Console.WriteLine("ProductID is : " + ProductID);
            Console.WriteLine("name: " + name);
            Console.WriteLine("Price: " + Price);
            Console.WriteLine("weightKg: " +weightKg);
            Console.WriteLine("shippingCost " + shippingCostPerKg);
        }
        public override double CalculateTotalCost()
        {
            return price + (weightKg *shippingCostPerKg);
        }


    }

    class DigitalProduct :Product
    {
        private double fileSizeMB;
        private string downloadLink;
        public DigitalProduct(string name, double price, double fileSizeMB, string downloadLink) : base(name, price)
        {
            this.fileSizeMB = fileSizeMB;
            this.downloadLink = downloadLink;
        }
           public override void DisplayInfo()
        {
            Console.WriteLine("=============Digital Product============");
            Console.WriteLine("Product ID: " + ProductID);
            Console.WriteLine("Product Name: " + name);
            Console.WriteLine("Price : " + price);
            Console.WriteLine("fileSize: " + fileSizeMB);
            Console.WriteLine("download link: " + downloadLink);
        }
        

    }


    abstract class User
    {
        private static int totalUsersCreated;
        //Protected Fields
        protected string fullName;
        protected string email;
        //Properties
        public string FullName
        {
            get { return fullName; }
        }
        public string Email
        {
            get { return email; } 
        }
        public User(string fullName, string email)
        {
            this.fullName = fullName;
            this.email = email;
            totalUsersCreated++;
        }
        public static int GetTotalUsersCreated()
        {
            return totalUsersCreated;
        }
        public abstract void DisplayInfo();
        

        

    }
       class Customer :User
    {
        private List<Order> orders;
        public Customer(string fullName, string email) : base(fullName, email) 
        {
            orders= new List<Order>();
        }
        public override void DisplayInfo()
        {
            Console.WriteLine($"[Customer] Name: {fullName} | Email: {email} | Orders: {orders.Count}");
        }

        public void AddOrder(Order order)
        {
            orders.Add(order);
        }
           
        public void RemoveOrder(int orderID)
        {
            orders.RemoveAll(o => o.OrderID == orderID);
        }

        public void DisplayOrderHistory()
        {  
                if(orders.Count==0)
                {
                    Console.WriteLine("No orders yet");
                    return;
                }
            foreach (var o in orders)
            {
                o.DisplayInfo();

            }
        }


    }

    class Admin :User
    {
        private string role;
        
        public Admin(string fullName, string email, string role) :base(fullName,email)
        {
            this.role = role;
        }
        public sealed override void DisplayInfo()
        {
            Console.WriteLine( $"[Admin] Name: {fullName} | Email: {email} | Role: {role}");
        }

            



}
    class Order
    {
        private static int nextOrderID = 5000;
        private Customer customer;
        private Product product;
        private double totalCost;

        //Auto-Implemented Read-Only Property
        public int OrderID { get; }
        public Customer Customer { get; }
        public double TotalCost { get; }
        public Order(Customer customer, Product product)
        {
            OrderID = nextOrderID++;
            this.customer = customer;
            this.product = product;
            totalCost = product.CalculateTotalCost();
        }
        public void DisplayInfo()
        {
            Console.WriteLine("=======Order=======");
            Console.WriteLine("Order Id: " + OrderID);
            Console.WriteLine("Customer Name: " + Customer.FullName);
            Console.WriteLine("Product Name: " + product.Name);
            Console.WriteLine("Total Cost: " + totalCost);
        }

    }
    class Store
    {
        public string StoreName
        {
            get;private set;
        }

        private List<Product> products;
        private List<Customer> customers;
        private List<Order> orders;
        public Store(string name)
        {
            StoreName = name;
            products = new List<Product>();
            customers = new List<Customer>();
            orders = new List<Order>(); 

        }

        public void AddPhysicalProduct(string name, double price, double weight, double shippingPerKg)
        {
            PhysicalProduct physicalProduct = new PhysicalProduct(name,price,weight,shippingPerKg);
            products.Add(physicalProduct);
            Console.WriteLine($"Physical product added successfully. ID: {physicalProduct.ProductID}");
        }   

        public void AddDigitalProduct(string name, double price, double fileSizeMB, string link)
        {
            DigitalProduct digitalProduct=new DigitalProduct(name,price,fileSizeMB,link);
            products.Add(digitalProduct);
            Console.WriteLine($"Digital product added successfuly . ID:{digitalProduct.ProductID} ");
        }

        public void DisplayAllProducts()
        {
            foreach(Product product in products)
            {
                product.DisplayInfo();
            }
        }

        public void RegisterCustomer(string fullName, string email)
        {
            Customer existingCustomer = customers.Find(x => x.Email == email);
            if(existingCustomer != null)
            {
                Console.WriteLine("Email already exists");
                return;
            }
            Customer customer=new Customer(fullName,email);
            customers.Add(customer);
            Console.WriteLine("Customer registered successfully ");

        }
        public Customer FindCustomer(string email)
        {
            return customers.Find(c => c.Email == email);
        }

        public void PlaceOrder(string email, int productID)
        {
            Customer customer=FindCustomer(email);
            if(customer == null)
            {
                Console.WriteLine("Customer not found");
                return;
            }        

            Product product =products.Find(p=>p.ProductID == productID);
            if(product == null)
            {
                Console.WriteLine("Product not found");
                return;
            }
            Order placeOrder = new Order(customer, product);
            orders.Add(placeOrder);
            customer.AddOrder(placeOrder);
            Console.WriteLine("Order placed successfully.");
            Console.WriteLine("Order ID: " + placeOrder.OrderID);
            Console.WriteLine("Total Cost: " + placeOrder.TotalCost);
        }
        public void CancelOrder(int orderID)
        {
            Order order=orders.Find(o => o.OrderID == orderID);
                if(order == null)
            {
                Console.WriteLine("order not found");
                return;
            }
                 order.Customer.RemoveOrder(orderID);
            orders.RemoveAll(o => o.OrderID == orderID);
            Console.WriteLine("Order cancelled successfully.");

        }
        public void DisplayCustomerOrders(string email)
        {
            Customer customer = FindCustomer(email);

            if (customer == null)
            {
                Console.WriteLine("Customer not found");
                return;
            }

            customer.DisplayInfo();

            customer.DisplayOrderHistory();
        }
        private int CountPhysicalProducts()
        {
            int physicalCount = 0;
            foreach (Product p in products)
            {
                if(p is PhysicalProduct)
                {
                    physicalCount++;
                }
                
            }
            return physicalCount;
        }
        private int CountDigitalProducts()
        {
            int digitalCount = 0;
            foreach (Product p in products)
            {
                if(p is DigitalProduct)
                {
                    digitalCount++;
                }
            }
            return digitalCount;
        }
        private double CalculateTotalRevenue()
        {
            double total = 0;

            foreach (Order o in orders)
            {
                total += o.TotalCost;
            }

            return total;
        }

        public void DisplayStatistics()
        {
            Console.WriteLine("===== Store Statistics =====");
            Console.WriteLine("Stor name: " + StoreName);
            Console.WriteLine("Total product" + products.Count);
            Console.WriteLine("Physical Products: " + CountPhysicalProducts());
            Console.WriteLine("Digital product : " + CountDigitalProducts());
            Console.WriteLine("Total Revenue: " + CalculateTotalRevenue());
        }
    }
}
