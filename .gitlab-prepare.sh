#!/bin/sh

# Stops on error
set -e

# Check if running script inside a GitLab runner. If not, set default values to environment variables.
if [ -z ${CI_RUNNER_ID+x} ]; then
  UNITY_DIR=$(dirname $(readlink -f "$0"))
fi

# Ensure required variables are set.
if [ -z ${UNITY_DIR+x} ]; then
  echo "'\$UNITY_DIR' variable not set."
  exit 1
fi

# Determine the version of Unity to use to build the project. Keep it in an environment for the next stages.
echo "Determine Unity version for next steps"
unity_version=$(sed -n "s/m_EditorVersion: \(.*\)/\1/p" "$UNITY_DIR/ProjectSettings/ProjectVersion.txt")
echo "Unity version is $unity_version"
echo "UNITY_VERSION=$unity_version" > prepare.env