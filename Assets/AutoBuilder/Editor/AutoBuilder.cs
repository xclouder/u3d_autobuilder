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

public class AutoBuilder
{

	[MenuItem("Build/* Start Build Package",false,1019)]
	public static void BuildPackage(){
		if(EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android){

			CIBuild.BuildPackage (BuildTarget.Android,BuildOptions.AcceptExternalModificationsToPlayer);
		}else{
			CIBuild.BuildPackage (EditorUserBuildSettings.activeBuildTarget);
		}
	}


	[MenuItem("Build/Build For Platform/Android", false, 1020)]
	public static void BuildAndroidPackage(){
		CIBuild.BuildPackage (BuildTarget.Android,BuildOptions.AcceptExternalModificationsToPlayer);
	}

	[MenuItem("Build/Build For Platform/Android", true, 1020)]
	static bool ValidateBuildAndroidPackage()
	{
		return EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android;
	}

	[MenuItem("Build/Build For Platform/iOS",false,1020)]
	public static void BuildiOSPackage(){
		#if UNITY_5
		CIBuild.BuildPackage (BuildTarget.iOS);
		#else
		CIBuild.BuildPackage (BuildTarget.iPhone);
		#endif
	}

	[MenuItem("Build/Build For Platform/iOS", true, 1020)]
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

	/*
	[MenuItem("JDKLib/Debug/Build Android Native Jar",false,2038)]
	public static void BuildJar()
	{
		AndroidNativeJarBuildScript.BuildJar ();
	}

	[MenuItem("JDKLib/PreBuild Project",false,1020)]
	public static void Prebuild(){
		JDKPreBuildProcessor.PreBuild ();
	}

	[MenuItem("JDKLib/Module Settings",false,1021)]
	public static void ModuleSettings()
	{
		JDKModuleSettings.Edit ();
	}

	[MenuItem("JDKLib/Debug/Export Eclipse Project",false,2046)]
	public static void BuildAndroidProject(){
		CIBuild.BuildPackage (BuildTarget.Android,BuildOptions.AcceptExternalModificationsToPlayer);
	}

	[MenuItem("JDKLib/Debug/Test CI Build iOS",false,2046)]
	public static void TestAutoBuildiOSProject(){
		CIBuild.CommandLineBuildiOS();
	}

	[MenuItem("JDKLib/Debug/Test CI Build Android",false,2046)]
	public static void TestAutoBuildAndroidProject(){
		CIBuild.CommandLineBuildAndroid();
	}
	*/

}