using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotNetAppPoc
{
    public class JiraTicketFetcher
    {
        private readonly string _mcpApiUrl;
        private readonly HttpClient _httpClient;

        public JiraTicketFetcher(string mcpApiUrl)
        {
            _mcpApiUrl = mcpApiUrl ?? throw new ArgumentNullException(nameof(mcpApiUrl));
            _httpClient = new HttpClient();
        }

        public async Task<JiraTicketDetails?> FetchTicketAsync(string issueKey)
        {
            try
            {
                var requestBody = new
                {
                    jsonrpc = "2.0",
                    id = 1,
                    method = "tools/call",
                    @params = new
                    {
                        name = "pss_fabric_get_jira_issue",
                        arguments = new
                        {
                            issue_key = issueKey
                        }
                    }
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Post, _mcpApiUrl)
                {
                    Content = content
                };
                request.Headers.Add("Accept", "application/json, text/event-stream");

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                
                // Parse the SSE response format
                var lines = responseBody.Split('\n');
                string? dataLine = null;
                foreach (var line in lines)
                {
                    if (line.StartsWith("data:"))
                    {
                        dataLine = line.Substring(5).Trim();
                        break;
                    }
                }

                if (dataLine == null)
                {
                    Console.WriteLine("Error: No data line found in response");
                    return null;
                }

                using var document = JsonDocument.Parse(dataLine);
                var root = document.RootElement;
                
                if (root.TryGetProperty("result", out var result) &&
                    result.TryGetProperty("content", out var contentArray) &&
                    contentArray.GetArrayLength() > 0)
                {
                    var contentText = contentArray[0].GetProperty("text").GetString();
                    if (contentText != null)
                    {
                        using var ticketDoc = JsonDocument.Parse(contentText);
                        return ParseJiraTicket(ticketDoc.RootElement);
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching JIRA ticket: {ex.Message}");
                return null;
            }
        }

        private JiraTicketDetails ParseJiraTicket(JsonElement ticketElement)
        {
            var details = new JiraTicketDetails
            {
                Key = GetStringProperty(ticketElement, "key"),
                Summary = GetStringProperty(ticketElement, "summary"),
                Status = GetStringProperty(ticketElement, "status"),
                Assignee = GetStringProperty(ticketElement, "assignee"),
                Priority = GetStringProperty(ticketElement, "priority"),
                Description = GetStringProperty(ticketElement, "description")
            };

            return details;
        }

        private string GetStringProperty(JsonElement element, string propertyName)
        {
            if (element.TryGetProperty(propertyName, out var property))
            {
                if (property.ValueKind == JsonValueKind.String)
                {
                    return property.GetString() ?? "N/A";
                }
                else if (property.ValueKind == JsonValueKind.Object && propertyName == "description")
                {
                    // Parse Atlassian Document Format (ADF) for description
                    return ExtractTextFromADF(property);
                }
                else if (property.ValueKind == JsonValueKind.Object)
                {
                    return property.ToString() ?? "N/A";
                }
            }
            return "N/A";
        }

        private string ExtractTextFromADF(JsonElement adfElement)
        {
            var textBuilder = new StringBuilder();
            
            if (adfElement.TryGetProperty("content", out var contentArray))
            {
                foreach (var contentItem in contentArray.EnumerateArray())
                {
                    if (contentItem.TryGetProperty("content", out var innerContent))
                    {
                        foreach (var item in innerContent.EnumerateArray())
                        {
                            if (item.TryGetProperty("text", out var textElement))
                            {
                                textBuilder.Append(textElement.GetString());
                            }
                        }
                        textBuilder.AppendLine();
                    }
                }
            }
            
            return textBuilder.ToString().Trim();
        }
    }

    public class JiraTicketDetails
    {
        public string Key { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Assignee { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public void Display()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("JIRA Ticket Details");
            Console.WriteLine("========================================");
            Console.WriteLine($"Key:         {Key}");
            Console.WriteLine($"Summary:     {Summary}");
            Console.WriteLine($"Status:      {Status}");
            Console.WriteLine($"Assignee:    {Assignee}");
            Console.WriteLine($"Priority:    {Priority}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine("========================================\n");
        }
    }
}
