using UnityEditor;
using System.IO;
using UnityEngine;
using System.Linq;


public static class CIBuild{
    public static string PrePath{
        get{
            return EditorPrefs.GetString("Build_preBuildPathCacheKey", "");
        }
        set{
			EditorPrefs.SetString("Build_preBuildPathCacheKey", value);
        }
    }

	#region private
	private static string GetPackageBuildPath()
	{
		string dirPath = Application.dataPath;
		DirectoryInfo f = new DirectoryInfo(dirPath);

		dirPath = Path.Combine(f.Parent.FullName, "bulids");
		if (!Directory.Exists(dirPath)){
			Directory.CreateDirectory(dirPath);
		}

		return dirPath;
	}

    private static string GetiOSBuildPath(){
		return GetPackageBuildPath();
    }

	private static string GetAndroidBuildPath(){
		return GetPackageBuildPath();
	}

	private static string[] GetBuildScenes(){
		return (from scene in EditorBuildSettings.scenes
			where scene.enabled
			select scene.path).ToArray();
	}

	#endregion

    public static void CommandLineBuildiOS(){
		BuildTarget target = BuildTarget.iOS;

		if (EditorUserBuildSettings.activeBuildTarget != target){
			EditorUserBuildSettings.SwitchActiveBuildTarget(target);
		}

		DoBuildPackage(BuildTarget.iOS, GetiOSBuildPath());
    }

    public static void CommandLineBuildAndroid(){
		BuildTarget target = BuildTarget.Android;

		if (EditorUserBuildSettings.activeBuildTarget != target){
			EditorUserBuildSettings.SwitchActiveBuildTarget(target);
		}

		var fileName = PlayerSettings.productName + ".apk";
		var p = Path.Combine(GetAndroidBuildPath(), fileName);

		Debug.Log("android package path:" + p);
		DoBuildPackage(BuildTarget.Android, p);
    }

	public static void DoBuildPackage(BuildTarget target, string path, BuildOptions option = BuildOptions.None)
	{
		//TODO:pre build
		Debug.Log("Prebuilding...");
		//....

		Debug.Log("Start BuildPlayer...");
		BuildPipeline.BuildPlayer(GetBuildScenes(), path, target, option);

		Debug.Log("Build completed");
	}

    public static void BuildPackage(BuildTarget target, BuildOptions option = BuildOptions.None){

		//init properties
		#if UNITY_IPHONE
		var bundleId = PlayerSettings.iPhoneBundleIdentifier;
		#else
		var bundleId = PlayerSettings.bundleIdentifier;
		#endif

		var shortVer = PlayerSettings.bundleVersion;
		var productName = PlayerSettings.productName;

        if (EditorApplication.isCompiling){
            EditorUtility.DisplayDialog("错误提示", "Unity Editor 正在编译代码,请编译完成后再构建.", "取消");
            return;
        } 

        if (EditorUserBuildSettings.activeBuildTarget != target){
            if (EditorUtility.DisplayDialog("目标平台切换", "需要先切换到:" + target.ToString() + " 平台才能继续构建,是否立即切换?", "立即切换", "取消构建")){
                Debug.LogWarning("SwitchActiveBuildTarget to " + target.ToString());
                if (!EditorUserBuildSettings.SwitchActiveBuildTarget(target)){
                    Debug.LogWarning("取消平台切换");
                    return;
                }
            } else{
                Debug.LogWarning("取消构建");
                return;
            }
        }

//        if (AutoBuilderSettings.BuildSettingsAlert(true)){
		if (true){
            string fileName;
            string ext;
			bool isDebugMode = false;

            if (target == BuildTarget.Android){
                if(!isDebugMode)
                    PlayerSettings.Android.bundleVersionCode += 1;
                if (BuildOptions.AcceptExternalModificationsToPlayer == option){
                    fileName = productName + "_" + shortVer;
                    ext = "";
                } else{
                    fileName = productName + "_" + shortVer + "_" + System.DateTime.Now.ToString("yyMMddHHmm") + ".apk";
                    ext = "apk";
                }
            } else{
                fileName =productName + "_" + shortVer;
                ext = "";
            }
            var path = EditorUtility.SaveFilePanel("保存到(选择已经存在的路劲会覆盖原有的包)", PrePath == "" ? "" : Path.GetDirectoryName(PrePath), fileName, ext);
            if (path.Length != 0){
                PrePath = path;

                Debug.LogWarning("path:" + path);
                if (path.IndexOf(Path.GetDirectoryName(Application.dataPath)) >= 0){
                    EditorUtility.DisplayDialog("错误提示", "不要构建到Unity工程目录下,避免污染项目目录.\n请取消后重新选择构建目录.", "取消");
                    return;
                }

				DoBuildPackage(target, path, option);

            } else{
                Debug.LogWarning("Cancel build.");
            }
        } else{
            Debug.LogWarning("Cancel build.");
        }
    }

}
