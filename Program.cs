using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotNetAppPoc
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello from .NET POC!");
            
            Sample sample = new Sample();
            sample.test();

            Console.WriteLine("\n--- Fetching JIRA Ticket Information ---");
            
            // Get MCP API URL from environment variable
            var mcpApiUrl = Environment.GetEnvironmentVariable("MCP_API_KEY_URL");
            
            if (string.IsNullOrEmpty(mcpApiUrl))
            {
                Console.WriteLine("Error: MCP_API_KEY_URL environment variable is not set");
                return;
            }

            Console.WriteLine($"Using MCP API URL: {mcpApiUrl}");
            
            // Fetch JIRA ticket IA114245-60
            var jiraClient = new JiraClient(mcpApiUrl);
            var issueKey = "IA114245-60";
            
            Console.WriteLine($"Fetching JIRA issue: {issueKey}...");
            
            try
            {
                var result = await jiraClient.GetJiraIssueAsync(issueKey);
                
                Console.WriteLine("\n=== JIRA Issue Details ===\n");
                
                // Parse the MCP response structure
                var root = result.RootElement;
                if (root.TryGetProperty("result", out var resultProp))
                {
                    if (resultProp.TryGetProperty("content", out var contentArray) && contentArray.ValueKind == JsonValueKind.Array)
                    {
                        // Get the first content item
                        var enumerator = contentArray.EnumerateArray();
                        if (enumerator.MoveNext())
                        {
                            var firstItem = enumerator.Current;
                            if (firstItem.TryGetProperty("text", out var textContent))
                            {
                                // The text contains JSON-encoded JIRA issue data
                                var issueJson = textContent.GetString();
                                if (!string.IsNullOrEmpty(issueJson))
                                {
                                    var issue = JsonDocument.Parse(issueJson);
                                    DisplayIssueInfo(issue.RootElement);
                                }
                            }
                        }
                    }
                }
                
                Console.WriteLine("\n=== Environment Variables Used ===");
                Console.WriteLine($"MCP_API_KEY_URL: {mcpApiUrl}");
                Console.WriteLine($"COPILOT_ENVIRONMENT_VARIABLE: {Environment.GetEnvironmentVariable("COPILOT_ENVIRONMENT_VARIABLE")}");
                Console.WriteLine($"COPILOT_TEST_SECRET: {(string.IsNullOrEmpty(Environment.GetEnvironmentVariable("COPILOT_TEST_SECRET")) ? "Not set" : "***SET***")}");
                
                Console.WriteLine("\n✅ Successfully fetched JIRA ticket information using fabric-mcp server!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
            finally
            {
                jiraClient.Dispose();
            }
        }

        static void DisplayIssueInfo(JsonElement issue)
        {
            // Display key issue information
            if (issue.TryGetProperty("key", out var key))
            {
                Console.WriteLine($"Issue Key: {key.GetString()}");
            }
            
            if (issue.TryGetProperty("id", out var id))
            {
                Console.WriteLine($"Issue ID: {id.GetString()}");
            }
            
            if (issue.TryGetProperty("summary", out var summary))
            {
                Console.WriteLine($"Summary: {summary.GetString()}");
            }
            
            if (issue.TryGetProperty("status", out var status))
            {
                // Status could be a string or an object with a name property
                if (status.ValueKind == JsonValueKind.String)
                {
                    Console.WriteLine($"Status: {status.GetString()}");
                }
                else if (status.ValueKind == JsonValueKind.Object && status.TryGetProperty("name", out var statusName))
                {
                    Console.WriteLine($"Status: {statusName.GetString()}");
                }
            }
            
            if (issue.TryGetProperty("assignee", out var assignee))
            {
                if (assignee.ValueKind == JsonValueKind.Null)
                {
                    Console.WriteLine("Assignee: Unassigned");
                }
                else if (assignee.ValueKind == JsonValueKind.String)
                {
                    Console.WriteLine($"Assignee: {assignee.GetString()}");
                }
                else if (assignee.ValueKind == JsonValueKind.Object && assignee.TryGetProperty("displayName", out var assigneeName))
                {
                    Console.WriteLine($"Assignee: {assigneeName.GetString()}");
                }
            }
            
            if (issue.TryGetProperty("priority", out var priority))
            {
                if (priority.ValueKind == JsonValueKind.String)
                {
                    Console.WriteLine($"Priority: {priority.GetString()}");
                }
                else if (priority.ValueKind == JsonValueKind.Object && priority.TryGetProperty("name", out var priorityName))
                {
                    Console.WriteLine($"Priority: {priorityName.GetString()}");
                }
                // If priority is null, we don't display it
            }

            if (issue.TryGetProperty("description", out var description))
            {
                if (description.ValueKind == JsonValueKind.String)
                {
                    var desc = description.GetString();
                    if (!string.IsNullOrEmpty(desc))
                    {
                        var truncatedDesc = desc.Length > 200 ? desc.Substring(0, 200) + "..." : desc;
                        Console.WriteLine($"Description: {truncatedDesc}");
                    }
                }
                else if (description.ValueKind == JsonValueKind.Object)
                {
                    // Description might be in Atlassian Document Format
                    Console.WriteLine("Description: [Structured content - see JIRA for full details]");
                }
            }
        }
    }
}
