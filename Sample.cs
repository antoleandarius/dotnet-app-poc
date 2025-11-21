using System;

namespace DotNetAppPoc
{
    public class Sample
    {
        // VIOLATION: Method name uses lowercase (should be PascalCase)
        public void test()
        {
            Console.WriteLine("bad naming");
        }

        // Another violation for testing
        public void calculate()
        {
            Console.WriteLine("also bad naming");
        }

        /// <summary>
        /// Calculates the discounted price based on the original price and discount percentage.
        /// </summary>
        /// <param name="originalPrice">The original price before discount</param>
        /// <param name="discountPercentage">The discount percentage to apply (0-100)</param>
        /// <returns>The final price after applying the discount</returns>
        public decimal CalculateDiscount(decimal originalPrice, decimal discountPercentage)
        {
            if (originalPrice < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(originalPrice), "Original price cannot be negative");
            }

            if (discountPercentage < 0 || discountPercentage > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(discountPercentage), "Discount percentage must be between 0 and 100");
            }

            decimal discountAmount = originalPrice * (discountPercentage / 100);
            return originalPrice - discountAmount;
        }
    }
}
