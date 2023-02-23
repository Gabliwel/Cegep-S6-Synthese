#!/bin/sh

# Stops the script on error.
set -e

# Ensure required variables are set.
if [ -z ${UNITY_LICENSE+x} ]; then
  echo "'\$UNITY_LICENSE' variable not set"
  exit 1
fi

# Create required directories.
echo "Create required directories" 
mkdir -p /root/.cache/unity3d               # Create the unity3d cache dir.
mkdir -p /root/.local/share/unity3d/Unity/  # Create the unity3d config dir.

# Set Unity license file path.
unity_license_destination=/root/.local/share/unity3d/Unity/Unity_lic.ulf

# Write the Unity licence file from the $UNITY_LICENSE environment variable.
# Strip Windows line endings in the process.
echo "Writing '\$UNITY_LICENSE' to ${unity_license_destination}" 
echo "${UNITY_LICENSE}" | tr -d '\r' > ${unity_license_destination}
