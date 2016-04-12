#!/bin/bash --login

echo "Creating symbolic links in Android SDK folder"
echo "============================================="
echo

build_tools_version="23.0.0"

if [ -z "$ANDROID_HOME" ] ; then
  echo "Could not find Android SDK. Environment variable ANDROID_HOME is not defined."
  exit 1
fi

if [ ! -d "$ANDROID_HOME/build-tools/$build_tools_version" ] ; then
  echo "Could not find build-tools folder $build_tools_version Please download the latest version first."
  exit 1
fi

echo "Found ANDROID_HOME set to $ANDROID_HOME"
echo

# For IntelliJ and Android-Studio

ln -s $ANDROID_HOME/platform-tools/adb $ANDROID_HOME/tools/adb
rm -f $ANDROID_HOME/tools/aapt
ln -s $ANDROID_HOME/build-tools/$build_tools_version/aapt $ANDROID_HOME/tools/aapt
rm -f $ANDROID_HOME/tools/dx
ln -s $ANDROID_HOME/build-tools/$build_tools_version/dx $ANDROID_HOME/tools/dx
rm -f $ANDROID_HOME/tools/zipalign
ln -s $ANDROID_HOME/build-tools/$build_tools_version/zipalign $ANDROID_HOME/tools/zipalign
rm -f $ANDROID_HOME/tools/dx.jar
ln -s $ANDROID_HOME/build-tools/$build_tools_version/lib/dx.jar $ANDROID_HOME/tools/dx.jar


path=`dirname "$0"`
target="$1"
cd "$target"
$path/../Android/gradle/bin/gradle clean
$path/../Android/gradle/bin/gradle assembleRelease