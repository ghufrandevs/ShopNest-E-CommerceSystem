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

    }
    abstract class User
    {

    }
}
