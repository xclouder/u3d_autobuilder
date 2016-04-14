/*************************************************************************
 *  FileName: AutoBuilder.cs
 *  Author: xClouder
 *  Create Time: #CreateTime#
 *  Description:
 *
 *************************************************************************/

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;

public class AutoBuilder
{

	[MenuItem("Build/* Start Build Package",false,1019)]
	public static void BuildPackage(){
		CIBuild.BuildPackage (EditorUserBuildSettings.activeBuildTarget);
	}

	[MenuItem("Build/Build for Platform/Android", false, 1020)]
	public static void BuildAndroidPackage(){
		CIBuild.BuildPackage (BuildTarget.Android);
	}

	[MenuItem("Build/Build for Platform/Android", true, 1020)]
	static bool ValidateBuildAndroidPackage()
	{
		return EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android;
	}

	[MenuItem("Build/Build for Platform/iOS",false,1020)]
	public static void BuildiOSPackage(){
		#if UNITY_5
		CIBuild.BuildPackage (BuildTarget.iOS);
		#else
		CIBuild.BuildPackage (BuildTarget.iPhone);
		#endif
	}

	[MenuItem("Build/Build for Platform/iOS", true, 1020)]
	static bool ValidateBuildiOSPackage()
	{
		#if UNITY_5
		return EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS;
		#else
		return EditorUserBuildSettings.activeBuildTarget == BuildTarget.iPhone;
		#endif
	}


	[MenuItem("Build/Android Build Settings",false,1022)]
	public static void ShowAndroidBuildSettings()
	{
		AndroidBuildSettings.Edit ();
	}

}