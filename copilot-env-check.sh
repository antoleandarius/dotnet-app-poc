#!/usr/bin/env bash

if [ -z "$COPILOT_TEST_SECRET" ]; then
  echo "❌ COPILOT_TEST_SECRET is NOT set"
else
  echo "✅ COPILOT_TEST_SECRET is set (value redacted): $COPILOT_TEST_SECRET"
fi

if [ -z "$COPILOT_ENVIRONMENT_VARIABLE" ]; then
  echo "❌ COPILOT_ENVIRONMENT_VARIABLE is NOT set"
else
  echo "✅ COPILOT_ENVIRONMENT_VARIABLE is set (value redacted): $COPILOT_ENVIRONMENT_VARIABLE"
fi
