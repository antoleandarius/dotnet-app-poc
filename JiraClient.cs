using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotNetAppPoc
{
    public class JiraClient : IDisposable
    {
        private const string DataPrefix = "data: ";
        private readonly string _mcpApiUrl;
        private readonly HttpClient _httpClient;
        private bool _disposed = false;

        public JiraClient(string mcpApiUrl)
        {
            _mcpApiUrl = mcpApiUrl ?? throw new ArgumentNullException(nameof(mcpApiUrl));
            _httpClient = new HttpClient();
        }

        public async Task<JsonDocument> GetJiraIssueAsync(string issueKey)
        {
            var request = new
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

            var jsonContent = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json, text/event-stream");

            try
            {
                var response = await _httpClient.PostAsync(_mcpApiUrl, content);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                
                // Parse the event stream response
                if (responseContent.StartsWith("event: message"))
                {
                    var lines = responseContent.Split('\n');
                    foreach (var line in lines)
                    {
                        if (line.StartsWith(DataPrefix))
                        {
                            var jsonData = line.Substring(DataPrefix.Length);
                            return JsonDocument.Parse(jsonData);
                        }
                    }
                }

                return JsonDocument.Parse(responseContent);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching JIRA issue: {ex.Message}", ex);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _httpClient?.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
