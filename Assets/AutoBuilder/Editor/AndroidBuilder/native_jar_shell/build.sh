#!/bin/bash --login
path=`dirname "$0"`
cd "$path"

target="$1"

#clear
rm -rf build

#build
../gradle/bin/gradle jarRelease

#copy jar
# cp build/libs/jdkplugin.jar ../../../../Plugins/Android/jdk-native.jar