#!/bin/sh

# Stops the script on error.
set -e

# Check if running script inside a GitLab runner. If not, set default values to environment variables.
if [ -z ${CI_RUNNER_ID+x} ]; then
  UNITY_DIR=$(dirname $(readlink -f "$0"))
  BUILD_NAME="DefaultProjectName"
  BUILD_TARGET="StandaloneWindows64" 
fi

# Create build folder.
echo "Create build directory"
export BUILD_PATH=$UNITY_DIR/Builds/$BUILD_TARGET/
mkdir -p $BUILD_PATH

# Call Unity to build the project. This expects a license and a "BuildCommand.cs" class.
echo "Building for $BUILD_TARGET."
${UNITY_EXECUTABLE:-xvfb-run --auto-servernum --server-args='-screen 0 640x480x24' unity-editor} \
  -projectPath $UNITY_DIR \
  -quit \
  -batchmode \
  -nographics \
  -buildTarget $BUILD_TARGET \
  -customBuildTarget $BUILD_TARGET \
  -customBuildName $BUILD_NAME \
  -customBuildPath $BUILD_PATH \
  -executeMethod BuildCommand.PerformBuild \
  -logFile $UNITY_DIR/build.log

# Check if build succeeded and report that.
UNITY_EXIT_CODE=$?
if [ $UNITY_EXIT_CODE -eq 0 ]; then
  echo "Run succeeded, no failures occurred";
elif [ $UNITY_EXIT_CODE -eq 2 ]; then
  echo "Run succeeded, some tests failed";
elif [ $UNITY_EXIT_CODE -eq 3 ]; then
  echo "Run failure (other failure)";
else
  echo "Unexpected exit code $UNITY_EXIT_CODE";
fi

# Fail job if build folder is empty.
ls -la $BUILD_PATH
[ -n "$(ls -A $BUILD_PATH)" ]
