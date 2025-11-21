using System;

namespace DotNetAppPoc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello from .NET POC!");
            
            Sample sample = new Sample();
            sample.test();

            // Test the CalculateDiscount method
            Console.WriteLine("\n--- Testing CalculateDiscount Method ---");
            decimal originalPrice = 100m;
            decimal discount = 20m;
            decimal finalPrice = sample.CalculateDiscount(originalPrice, discount);
            Console.WriteLine($"Original Price: ${originalPrice}");
            Console.WriteLine($"Discount: {discount}%");
            Console.WriteLine($"Final Price: ${finalPrice}");
        }
    }
}
