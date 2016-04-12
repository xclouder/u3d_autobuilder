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
    private static string GetiOSBuildPath(){
        string dirPath = Application.dataPath + "/../../../build/iPhone";
        Debug.LogWarning("Build Dir:" + dirPath);
        if (!Directory.Exists(dirPath)){
            Directory.CreateDirectory(dirPath);
        }
        return Path.GetFullPath(dirPath);
    }

	private static string GetAndroidBuildPath(){
		string dirPath = Application.dataPath + "/../../../build/android";
		if (!Directory.Exists(dirPath)){
			Directory.CreateDirectory(dirPath);
		}

		var fullPath = Path.GetFullPath(dirPath);
		Debug.Log("Android Build Path:" + fullPath);

		return fullPath;
	}

	private static string[] GetBuildScenes(){
		return (from scene in EditorBuildSettings.scenes
			where scene.enabled
			select scene.path).ToArray();
	}

	#endregion

    public static void CommandLineBuildiOS(){
		throw new System.NotImplementedException();
    }

    public static void CommandLineBuildAndroid(){
        BuildTarget target = BuildTarget.Android;

        if (EditorUserBuildSettings.activeBuildTarget != target){
           EditorUserBuildSettings.SwitchActiveBuildTarget(target);
        }

//	    JDK.EditorUtils.JDKMenus.Prebuild();
//        Debug.Log("Command line build android version\n------------------\n------------------");

        string[] scenes = GetBuildScenes();
        string path = GetAndroidBuildPath();
        if (scenes == null || scenes.Length == 0 || path == null)
            return;

//        Debug.Log(string.Format("Path: \"{0}\"", path));
//        for (int i = 0; i < scenes.Length; ++i){
//            Debug.Log(string.Format("Scene[{0}]: \"{1}\"", i, scenes[i]));
//        }

		var bundleId = "r2games.bundle.test";
		var shortVer = "1.0";
		var productName = "productName";

        Debug.Log("Starting Android Build!");
		string[] bundleIdSplit = bundleId.Split('.');
		string packageName = bundleIdSplit[bundleIdSplit.Count()-1];
		string buildPath = path+"/"+packageName+ "_" + shortVer;
        string finalPath = buildPath + "_" + System.DateTime.Now.ToString("yyMMddHHmm") + ".apk";
        BuildPipeline.BuildPlayer(scenes, buildPath, BuildTarget.Android, BuildOptions.AcceptExternalModificationsToPlayer);

        BuildAPKPackage(buildPath,false,finalPath);
    }

    public static void BuildPackage(BuildTarget target, BuildOptions option = BuildOptions.None){
		var bundleId = "r2games.bundle.test";
		var shortVer = "1.0";
		var productName = "productName";

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
//        if (JDKPlayerSettingsEditor.BuildSettingsAlert(true)){
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
            var path = EditorUtility.SaveFilePanel("保存到(选择已经存在的路劲会覆盖原有的包,文件名:" + fileName + ")", PrePath == "" ? "" : Path.GetDirectoryName(PrePath), fileName, ext);
            if (path.Length != 0){
                PrePath = path;

                Debug.LogWarning("path:" + path);
                if (path.IndexOf(Path.GetDirectoryName(Application.dataPath)) >= 0){
                    EditorUtility.DisplayDialog("错误提示", "不要构建到Unity工程目录下,避免污染项目目录.\n请取消后重新选择构建目录.", "取消");
                    return;
                }

				//TODO:pre build
//                JDK.EditorUtils.JDKPreBuildProcessor.PreBuild();
                Debug.LogWarning("Start BuildPlayer.");
                BuildPipeline.BuildPlayer(GetBuildScenes(), path, target, option);
                //调用项目自定义钩子
                string buildedPath = target == BuildTarget.Android ? Path.Combine(path, productName) : path;
                
                if(BuildOptions.AcceptExternalModificationsToPlayer == option && target == BuildTarget.Android){
                    BuildAPKPackage(path,true);
                }else{
                    OpenInFileBrowser.Open(path);
                }
            } else{
                Debug.LogWarning("Cancel build.");
            }
        } else{
            Debug.LogWarning("Cancel build.");
        }
    }

    private static void BuildAPKPackage(string path,bool isOpenFolderOnFinish,string finishAPKPath=""){
		var bundleId = "r2games.bundle.test";
		var shortVer = "1.0";
		var productName = "productName";

        //使用gradle构建apk包
        //Unity导出android工程会自动加项目名作为最终目录，把它移出来
        var info = new DirectoryInfo (Path.Combine(path, productName));
        foreach (FileSystemInfo fsi in info.GetFileSystemInfos()) {
            string destName = Path.Combine (path, fsi.Name);
            if(File.Exists(destName)||Directory.Exists(destName)){
                Debug.LogWarning("remove old :"+destName);
                FileLinkUtil.ForceRemove(destName);
            }
            if (fsi is FileInfo) {
                Debug.Log("move :"+fsi.FullName+" to :"+destName);
                File.Move(fsi.FullName, destName) ;
            }else{
                Debug.Log("move :"+fsi.FullName+" to :"+destName);
                Directory.Move(fsi.FullName,destName) ;
            }
        }
        Directory.Delete(Path.Combine(path, productName));
        File.Copy(Application.dataPath + "/JDKLib/Editor/Tools/apk_build.gradle",
            Path.Combine(path, "build.gradle"),true);
        File.Copy(Path.Combine (Application.dataPath, "JDKLib/Editor/Android/ext/releasesig.keystore"),
            Path.Combine(path, "releasesig.keystore"),true);
        Debug.Log ("===========start APK 构建==============");
        EditorUtility.DisplayProgressBar(
            "正在构建APK包",
            "请等待1-2分钟,首次构建时间可能需要5+分钟",
            0.8f);
        var proc = new System.Diagnostics.Process();
        proc.StartInfo.UseShellExecute = false;
        proc.StartInfo.CreateNoWindow = true;
        
        proc.StartInfo.RedirectStandardError = true;
        proc.EnableRaisingEvents=false; 
        proc.StartInfo.FileName = Application.dataPath + "/JDKLib/Editor/Tools/apk_build.sh";
        proc.StartInfo.Arguments = "\""+path+"\"";
        proc.Start();
        var err = proc.StandardError.ReadToEnd();
        proc.WaitForExit();
        EditorUtility.ClearProgressBar ();
        if (proc.ExitCode != 0) {
            Debug.LogError ("APK build error: " + err + "   code: " + proc.ExitCode);
            throw new System.Exception ("APK 构建失败");
        } else {
            var files = Directory.GetFiles(path+"/build/outputs/apk/", "*unaligned.apk", System.IO.SearchOption.AllDirectories);
            foreach (var f in files) 
            {
                Debug.LogWarning("remove unaligned file :"+f);
                File.Delete(f);
            }
            if(!string.IsNullOrEmpty(finishAPKPath)){
                var apkFiles = Directory.GetFiles(path+"/build/outputs/apk/", "*.apk", System.IO.SearchOption.AllDirectories);
                if(apkFiles.Length>0){
                    File.Move(apkFiles[0],finishAPKPath);
                }else{
                    Debug.LogError("no apk file found.");   
                }
            }
            Debug.Log ("APK 构建成功.");
            Debug.Log ("==============================================================");
            if(isOpenFolderOnFinish)
                OpenInFileBrowser.Open(path+"/build/outputs/apk/");
        }
    }
}
