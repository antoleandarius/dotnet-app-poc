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

        // Yet another lowercase method - testing Copilot review
        public void process()
        {
            Console.WriteLine("another violation");
        }
    }
}
