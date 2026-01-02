# .NET POC for GitHub Copilot Code Review

This is a sample .NET console application used to demonstrate GitHub Copilot Code Review with external coding guidelines.

## Purpose

This project intentionally contains naming violations to test whether GitHub Copilot Code Review can:
1. Read external coding guidelines from a separate repository
2. Enforce those guidelines during PR reviews
3. Flag violations based on the injected context

## Known Violations

The `Sample.cs` file contains lowercase method names (`test()`, `calculate()`) which violate the PascalCase naming convention defined in the guidelines repository.

## Building

```bash
dotnet build
dotnet run
```

## Additional Documentation

- [Fabric MCP Server Tools](FABRIC_MCP_SERVER_TOOLS.md) - List of available tools for Jira and Xray integration
