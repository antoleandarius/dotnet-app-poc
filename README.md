# .NET POC for GitHub Copilot Code Review

This is a sample .NET console application used to demonstrate GitHub Copilot Code Review with external coding guidelines and JIRA integration.

## Purpose

This project demonstrates:
1. Reading external coding guidelines from a separate repository
2. Enforcing those guidelines during PR reviews
3. Flagging violations based on the injected context
4. **Integration with fabric-mcp-server to fetch JIRA ticket details**

## Features

### JIRA Ticket Integration
The application uses the fabric-mcp-server to fetch JIRA ticket details. It demonstrates:
- Making authenticated requests to the fabric MCP API
- Parsing JSON-RPC 2.0 responses
- Handling Server-Sent Events (SSE) response format
- Extracting and displaying JIRA ticket information including:
  - Ticket key
  - Summary
  - Status
  - Assignee
  - Priority
  - Description (with ADF format parsing)

### Example Output
```
Hello from .NET POC!
Fetching JIRA ticket details using fabric-mcp-server...

========================================
JIRA Ticket Details
========================================
Key:         IA114245-60
Summary:     Add GET endpoint to retrieve task count by status
Status:      Open
Assignee:    Anto Lean Darius
Priority:    None
Description: DONOT DELETE THIS TASK: DotNET Sample Task to test Coding Agent.
             Create a lightweight GET endpoint...
========================================
```

## Known Violations

The `Sample.cs` file contains lowercase method names (`test()`, `calculate()`) which violate the PascalCase naming convention defined in the guidelines repository.

## Prerequisites

- .NET 8.0 SDK
- `MCP_API_KEY_URL` environment variable set to the fabric-mcp-server endpoint

## Building

```bash
dotnet build
dotnet run
```

## Environment Variables

- `MCP_API_KEY_URL`: The URL of the fabric-mcp-server API endpoint (e.g., `https://api-mcp-server-dev.azure-api.net/fabric/mcp`)

