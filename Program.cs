using Microsoft.Win32;
using System;
using System.Security.Cryptography.X509Certificates;

namespace ShopNest_E_CommerceSystem
{
    internal class Program
    {
        public static bool ExitSystem()
        {
            Console.WriteLine("Are you sure you want to exit the system? (yes/no)");
            string inputs = (Console.ReadLine() ?? string.Empty).ToLower();
            if (inputs == "no")
            {
                return false;
            }
            else if (inputs == "yes")
            {
                Console.WriteLine("Thank you for using the system --Exit--");
                return true;
            }
            else
            {
                Console.WriteLine("Invalid option");
                return false;
            }
        }
        static public void ShowMenue()
        {
            Console.WriteLine("========== Hello to ShopNest E-Commerce System ==========");
            Console.WriteLine("1.Add Physical Product");
            Console.WriteLine("2.Add Digital Product");
            Console.WriteLine("3.Display All Products");
            Console.WriteLine("4.Register Customer");
            Console.WriteLine("5.Place Order");
            Console.WriteLine("6.Cancel Order");
            Console.WriteLine("7.View Customer Order History");
            Console.WriteLine("8.Show Store Statistics");
            Console.WriteLine("0.Exit");
            Console.WriteLine("Enter your choice: ");




        }

        static public void  AddDigitalProduct(Store store)
        {
            Console.WriteLine("Enter Name : ");
            string name = (Console.ReadLine() ?? string.Empty).Trim();
            while (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty. Please re-enter:");
                name = (Console.ReadLine() ?? string.Empty).Trim();
            }

            Console.WriteLine("Enter price : ");
            double price;
            while (!double.TryParse(Console.ReadLine(), out price))
            {
                Console.WriteLine("Invalid input. Please enter a valid number:");
            }

            Console.WriteLine("Enter fileSizeMB : ");
            double fileSizeMB;
            while (!double.TryParse(Console.ReadLine(), out fileSizeMB))
            {
                Console.WriteLine("Invalid input. Please enter a valid number:");
            }

            Console.WriteLine("Enter link : ");
            string link = (Console.ReadLine() ?? string.Empty).Trim();
            while (string.IsNullOrWhiteSpace(link))
            {
                Console.WriteLine("Name cannot be empty. Please re-enter:");
                link = (Console.ReadLine() ?? string.Empty).Trim();
            }
            store.AddDigitalProduct(name, price, fileSizeMB,link);

        }
        static public void AddPhysicalProduct(Store store)
        {
            Console.WriteLine("Enter Name : ");
            string name = (Console.ReadLine() ?? string.Empty).Trim();
            while (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty. Please re-enter:");
                name = (Console.ReadLine() ?? string.Empty).Trim();
            }

            Console.WriteLine("Enter price : ");
            double price;
            while (!double.TryParse(Console.ReadLine(), out price))
            {
                Console.WriteLine("Invalid input. Please enter a valid number:");
            }
            
            Console.WriteLine("Enter weightKg : ");
            double weightKg;
            while (!double.TryParse(Console.ReadLine(), out weightKg))
            {
                Console.WriteLine("Invalid input. Please enter a valid number:");
            }

            Console.WriteLine("Enter shippingCostPerKg: ");
            double shippingCostPerKg;
            while (!double.TryParse(Console.ReadLine(), out shippingCostPerKg))
            {
                Console.WriteLine("Invalid input. Please enter a valid number:");
            }
            store.AddPhysicalProduct(name, price, weightKg, shippingCostPerKg);

        }
        //string fullName, string email
        static public void RegisterCustomer(Store store)
        {
            Console.WriteLine("Enter FullName : ");
            string FullName = (Console.ReadLine() ?? string.Empty).Trim();
            while (string.IsNullOrWhiteSpace(FullName))
            {
                Console.WriteLine("Name cannot be empty. Please re-enter:");
                FullName = (Console.ReadLine() ?? string.Empty).Trim();
            }
            string Email;
            while(true)
            {
                Console.WriteLine("Enter Email : ");
                Email = (Console.ReadLine() ?? string.Empty).Trim();
                while (string.IsNullOrWhiteSpace(Email))
                {
                    Console.WriteLine("Email cannot be empty. Please re-enter:");
                    Email = (Console.ReadLine() ?? string.Empty).Trim();
                }
                if (store.FindCustomer(Email) != null)
                {
                    Console.WriteLine("Email already exists. Try another email.");
                    continue;
                }
                break;
            }
            
             store.RegisterCustomer(FullName, Email);

        }



        static void Main(string[] args)
        {
            Store store=new Store("ShopNest");

            int option = 0;
            bool exit= false;
            while(!exit)
            {
                ShowMenue();
                try
                {
                    option = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please choose a number from 0 to 8");
                    continue;
                }
                switch(option)
                {
                    case 1://Add Physical Product
                        AddPhysicalProduct(store);
                        break;

                        case 2: //Add Digital Product
                        AddDigitalProduct(store);
                        break;

                        case 3://Display All Products
                        store.DisplayAllProducts();
                        break;

                        case 4://Register Customer
                        RegisterCustomer(store);
                        break;

                        case 5:
                        break;
                        case 6:
                        break;
                        case 7:
                        break;
                        case 8:
                        break;
                        case 0:
                        exit=ExitSystem();
                        break; 
                }
                if (!exit)
                {
                    Console.ReadLine();
                    Console.Clear();
                }

            }

        }
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
            Console.WriteLine("Total Cost: " + CalculateTotalCost());
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
            Console.WriteLine("Total Cost: " + CalculateTotalCost());
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
        public Customer Customer
        {
            get { return customer; }
        }
        public double TotalCost
        {
            get { return totalCost; }
        }


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
            Console.WriteLine("Store name: " + StoreName);
            Console.WriteLine("Total product" + products.Count);
            Console.WriteLine("Physical Products: " + CountPhysicalProducts());
            Console.WriteLine("Digital product : " + CountDigitalProducts());
            Console.WriteLine("Total Revenue: " + CalculateTotalRevenue());
            Console.WriteLine("Registered Customers: " + customers.Count);
            Console.WriteLine("Total Orders: " + orders.Count);
            Console.WriteLine(  "Total Users Created: " + User.GetTotalUsersCreated() );
        }
    }
}
