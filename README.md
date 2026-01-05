# .NET POC for GitHub Copilot Code Review

This is a sample .NET console application used to demonstrate GitHub Copilot Code Review with external coding guidelines and fabric-mcp server integration.

## Purpose

This project demonstrates:
1. GitHub Copilot Code Review with external coding guidelines
2. Integration with fabric-mcp server to fetch JIRA ticket information
3. Using environment variables and secrets for secure API access

## Features

### Code Review Testing
The `Sample.cs` file contains lowercase method names (`test()`, `calculate()`) which violate the PascalCase naming convention defined in the guidelines repository. This tests whether GitHub Copilot Code Review can:
1. Read external coding guidelines from a separate repository
2. Enforce those guidelines during PR reviews
3. Flag violations based on the injected context

### JIRA Integration via fabric-mcp Server
The application fetches JIRA ticket information (IA114245-60) using the fabric-mcp server. This demonstrates:
- Integration with Model Context Protocol (MCP) servers
- Secure API communication using environment variables
- Parsing and displaying JIRA ticket details

## Building and Running

```bash
dotnet build
dotnet run
```

## Environment Variables

The application uses the following environment variables:
- `MCP_API_KEY_URL` - The fabric-mcp server endpoint URL
- `COPILOT_ENVIRONMENT_VARIABLE` - Example environment variable
- `COPILOT_TEST_SECRET` - Example secret (value is masked in output)

## Implementation

- `Program.cs` - Main application entry point with JIRA ticket fetching
- `JiraClient.cs` - Client for communicating with fabric-mcp server
- `Sample.cs` - Sample class with intentional naming violations
- `copilot-env-check.sh` - Script to verify MCP server connectivity
