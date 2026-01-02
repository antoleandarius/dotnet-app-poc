# .NET POC for GitHub Copilot Code Review

This is a sample .NET console application used to demonstrate GitHub Copilot Code Review with external coding guidelines.

## Purpose

This project intentionally contains naming violations to test whether GitHub Copilot Code Review can:
1. Read external coding guidelines from a separate repository
2. Enforce those guidelines during PR reviews
3. Flag violations based on the injected context

## Known Violations

The `Sample.cs` file contains lowercase method names (`test()`, `calculate()`) which violate the PascalCase naming convention defined in the guidelines repository.

## New Feature: Task Management API

The application now includes a RESTful API endpoint for task management:

### Endpoint: GET /api/taskmgmt/task/count/by-status

Returns the count of tasks grouped by their status.

**Query Parameters:**
- `site` (optional): Filter tasks by site

**Response Format:**
```json
{
  "Open": 2,
  "InProgress": 1,
  "Completed": 2,
  "Cancelled": 1
}
```

**Example Usage:**
```bash
# Get all task counts
curl http://localhost:5000/api/taskmgmt/task/count/by-status

# Get task counts for a specific site
curl "http://localhost:5000/api/taskmgmt/task/count/by-status?site=Site%20A"
```

## Building

```bash
dotnet build
dotnet run
```

The API will be available at `http://localhost:5000` by default. Swagger documentation is available at `http://localhost:5000/swagger` when running in development mode.
