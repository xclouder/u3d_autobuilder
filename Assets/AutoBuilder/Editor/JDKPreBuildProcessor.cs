using UnityEngine;
using System;
using System.Collections;
using System.IO;

using UnityEditor;


    public class JDKPreBuildProcessor : MonoBehaviour{

        public static void PreBuild(){
            #if UNITY_EDITOR
            if (EditorApplication.isCompiling){
                EditorUtility.DisplayDialog("错误提示", "Unity Editor 正在编译代码,请编译完成后再构建.", "取消");
                throw new System.Exception("Unity Editor 正在编译代码,请编译完成后再构建.");
                return;
            }

            //调用项目自定义的钩子
//            if(!JDKModuleSettings.CustomBuildAction(JDKModuleSettings.BuildActionType.BeforPreBuild)){
//            	Debug.LogError("调用自定义钩子失败");
//            	return;
//            }

            Debug.Log("========= START PROJECT PRE BUILD ========");
            //1.首先把playersetting的bundleid 和应用名称更新到和配置文件一致
			//ignore now

            //2.Native Config Build
			//ignore now.

            //3.proj setting replace
			//ignore now

            //4.Native Source Build
			#if UNITY_IOS
			#endif

			#if UNITY_ANDROID
			AndroidNativeJarBuildScript.BuildJar();
			#endif
			
            //5.拷贝link.xml,以支持stripbyte优化模式
			//ignore now

            #endif
        }

        //[MenuItem("JDKLib/Update Player Setting",false,1021)]
        public static void UpdatePlayerSetting(){

		/*
            UnityEditor.PlayerSettings.productName = Config.shareInstance.ProductName;
            Debug.LogWarning("ProductName replace to:" + Config.shareInstance.ProductName);
            UnityEditor.PlayerSettings.bundleIdentifier = Config.shareInstance.GetBundleId;
            Debug.LogWarning("BundleIdentifier replace to:" + Config.shareInstance.GetBundleId);
            UnityEditor.PlayerSettings.bundleVersion = Config.shareInstance.ShortVersion;
            Debug.LogWarning("BundleVersion replace to:" + Config.shareInstance.ShortVersion);
            #if UNITY_4_6_3||UNITY_4_6_4||UNITY_4_6_5||UNITY_4_6_6||UNITY_4_6_7
            UnityEditor.PlayerSettings.shortBundleVersion = Config.shareInstance.ShortVersion;
            Debug.LogWarning("ShortBundleVersion replace to:" + Config.shareInstance.ShortVersion);
            #endif
		*/
            #if UNITY_ANDROID
			UnityEditor.PlayerSettings.Android.preferredInstallLocation = AndroidPreferredInstallLocation.Auto;
			Debug.LogWarning ("PreferredInstallLocation replace to: Auto");
			UnityEditor.PlayerSettings.Android.keystoreName = Path.Combine (Application.dataPath, "JDKLib/Editor/Android/ext/releasesig.keystore");
			Debug.LogWarning ("keystoreName replace to:"+Path.Combine (Application.dataPath, "JDKLib/Editor/Android/ext/releasesig.keystore"));
			UnityEditor.PlayerSettings.Android.keystorePass = "JiDanKe2013";
			Debug.LogWarning ("keystorePass replace to: JiDanKe2013");
			UnityEditor.PlayerSettings.Android.keyaliasName = "release";
			Debug.LogWarning ("keyaliasName replace to: release");
			UnityEditor.PlayerSettings.Android.keyaliasPass = "JiDanKe2013";
			Debug.LogWarning ("keyaliasPass replace to: JiDanKe2013");
            #endif
        }


    }
