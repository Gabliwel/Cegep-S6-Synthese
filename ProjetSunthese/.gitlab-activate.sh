#!/bin/sh

# Stops on error
set -e

# Configuration
UNITY_VERSION=2022.2.3f1

# Username and password of the account to use to activate.
unity_username=$1
unity_password=$2

if [ -z ${unity_username+x} ]; then
    echo "Unity username (first param of this script) is not set"
    exit -1
fi

if [ -z ${unity_password+x} ]; then
    echo "Unity username (second param of this script) is not set"
    exit -1
fi


# Start the CI docker image. Then, ask Unity to provide a manual activation file. 
# This will output an XML file. Save it as "Unity3D.alf".
docker run -it --rm \
-e "TEST_PLATFORM=linux" \
unityci/editor:$UNITY_VERSION-windows-mono-1 \
bash -c \
"unity-editor \
-batchmode \
-nographics \
-createManualActivationFile \
-username \"$unity_username\" -password \"$unity_password\" > /dev/null; \
echo $unity_username; \
cat /*.alf"

# Manually activate Unity using "Unity3D.alf" at : https://license.unity3d.com/manual
# You'll get a file named "Unity_v2022.x.ulf". Put the content of that file inside
# the UNITY_LICENSE environment variable in GitLab.