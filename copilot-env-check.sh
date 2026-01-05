#!/usr/bin/env bash

# if [ -z "$COPILOT_TEST_SECRET" ]; then
#   echo "❌ COPILOT_TEST_SECRET is NOT set"
# else
#   echo "✅ COPILOT_TEST_SECRET is set (value redacted): $COPILOT_TEST_SECRET"
# fi

# if [ -z "$COPILOT_ENVIRONMENT_VARIABLE" ]; then
#   echo "❌ COPILOT_ENVIRONMENT_VARIABLE is NOT set"
# else
#   echo "✅ COPILOT_ENVIRONMENT_VARIABLE is set (value redacted): $COPILOT_ENVIRONMENT_VARIABLE"
# fi

if [ -z "$MCP_API_KEY_URL" ]; then
  echo "❌ MCP_API_KEY_URL is NOT set"
  exit 1
fi

HTTP_CODE=$(curl -s -o response.json -w "%{http_code}" \
  -X POST "$MCP_API_KEY_URL" \
  -H "accept: application/json, text/event-stream" \
  -H "content-type: application/json" \
  --data '{
    "jsonrpc": "2.0",
    "id": 2,
    "method": "tools/list",
    "params": {}
  }')

if [ "$HTTP_CODE" = "200" ]; then
  echo "✅ MCP call succeeded using env URL + env secret"
else
  echo "❌ MCP call failed with HTTP $HTTP_CODE"
fi

