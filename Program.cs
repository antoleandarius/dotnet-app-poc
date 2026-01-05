using System;
using System.Threading.Tasks;

namespace DotNetAppPoc
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello from .NET POC!");
            Console.WriteLine("Fetching JIRA ticket details using fabric-mcp-server...\n");
            
            // Get MCP API URL from environment variable
            var mcpApiUrl = Environment.GetEnvironmentVariable("MCP_API_KEY_URL");
            
            if (string.IsNullOrEmpty(mcpApiUrl))
            {
                Console.WriteLine("Error: MCP_API_KEY_URL environment variable is not set.");
                return;
            }

            // Fetch JIRA ticket IA114245-60
            using var fetcher = new JiraTicketFetcher(mcpApiUrl);
            var ticket = await fetcher.FetchTicketAsync("IA114245-60");
            
            if (ticket != null)
            {
                ticket.Display();
            }
            else
            {
                Console.WriteLine("Failed to fetch JIRA ticket details.");
            }
            
            Console.WriteLine("\nRunning sample test...");
            Sample sample = new Sample();
            sample.test();
        }
    }
}
