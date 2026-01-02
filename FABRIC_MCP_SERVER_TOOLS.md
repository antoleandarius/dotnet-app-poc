# Fabric MCP Server - Available Tools

This document lists the available tools provided by the fabric-mcp-server.

## Available Tools

### 1. pss_fabric_get_jira_issue

Fetch a Jira issue by its key with all fields including custom fields.

**Parameters:**
- `issue_key` (string, required): The Jira issue key (e.g., 'PROJ-123')

**Returns:**
A dictionary containing the issue details with custom field names resolved.

**Example Usage:**
```
issue_key: "PROJ-123"
```

---

### 2. pss_fabric_get_xray_test_case

Fetch an Xray test case by its key.

**Parameters:**
- `test_case_key` (string, required): The Xray test case key (e.g., 'XSP-123')

**Returns:**
A dictionary containing the test case details.

**Example Usage:**
```
test_case_key: "XSP-123"
```

---

## About fabric-mcp-server

The fabric-mcp-server provides integration with Jira and Xray (a test management tool for Jira). These tools allow you to:

- Retrieve detailed information about Jira issues, including custom fields
- Access Xray test case information directly

## Notes

- All custom field names in Jira issues are automatically resolved for easier reading
- The tools return dictionary objects that can be parsed and processed as needed
