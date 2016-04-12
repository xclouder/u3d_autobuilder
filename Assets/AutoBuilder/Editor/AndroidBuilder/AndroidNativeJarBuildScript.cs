using UnityEngine;

using UnityEditor.Callbacks;
using UnityEditor;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;

	public class AndroidNativeJarBuildScript : MonoBehaviour {
    
		static string[] excludeFilesRegex= new string[] {
			@"^.*\.meta$",
			@"^.gitignore$",
			@"^\.DS_Store$",
			@"^.*\.bak$"
            };
	#if UNITY_EDITOR
		public static void BuildJar()
		{
			CleanOldDependency();
            PreReadyCompileEvn ();
			UpdateJavaSrcDirs ();
            //构建jdk-native.jar
			DoBuildJar ();
		}

		public static void CleanOldDependency(){

		}

        public static void PreReadyCompileEvn(){

        }

		static void UpdateJavaSrcDirs(){
			string modulePath = System.IO.Path.Combine (Application.dataPath, "JDKLib/Editor/Android/jdkplugin/build_template.gradle");
			string buildPath = System.IO.Path.Combine (Application.dataPath, "JDKLib/Editor/Android/jdkplugin/build.gradle");
			string subModulesKey = "${SUB_MODULES}";

			string addedModuleSrcs="";
//			foreach (var moduleName in JDKModuleSettings.Instance.OpenedFields()) 
//			{
//				addedModuleSrcs += "'src/main/"+moduleName+"',";
//			}

			string fileText = System.IO.File.ReadAllText (modulePath);
			fileText = fileText.Replace (subModulesKey, addedModuleSrcs.Substring(0, addedModuleSrcs.Length-1));
			System.IO.File.WriteAllText(buildPath, fileText);
		}

		static void DoBuildJar()
		{
			Debug.Log ("================== Build Android Native Jar ==================");
			
			Debug.Log ("building jar...");
			EditorUtility.DisplayProgressBar(
				"正在构建JDK Native jar",
				"请等待1-2分钟,首次构建时间可能需要10+分钟",
				0.8f);

			string path = Application.dataPath + "/JDKLib/Editor/Android/gradle_shell/";
			
			System.Diagnostics.Process proc = new System.Diagnostics.Process();
			proc.StartInfo.UseShellExecute = false;
			proc.StartInfo.CreateNoWindow = true;
			
			proc.StartInfo.RedirectStandardError = true;
			proc.EnableRaisingEvents=false; 
			proc.StartInfo.FileName = "chmod";
			proc.StartInfo.Arguments = "755 \"" + path + "build.sh\"";
			proc.Start();
			
			string err = proc.StandardError.ReadToEnd();
			proc.WaitForExit();
			
			proc = new System.Diagnostics.Process();
			proc.StartInfo.UseShellExecute = false;
			proc.StartInfo.CreateNoWindow = true;
			
			proc.StartInfo.RedirectStandardError = true;
			proc.EnableRaisingEvents=false; 
			proc.StartInfo.FileName = path + "build.sh";
//			if (Config.shareInstance.UseMSDK) {
//				proc.StartInfo.Arguments = "msdk";
//			}else{
//				proc.StartInfo.Arguments = "nonemsdk";
//			}
			proc.Start();
			
			err = proc.StandardError.ReadToEnd();
			proc.WaitForExit();
			EditorUtility.ClearProgressBar ();
            if (proc.ExitCode != 0) {
                UnityEngine.Debug.LogError ("error: " + err + "   code: " + proc.ExitCode);
                throw new System.Exception ("jdk jar 构建失败");
            } else {
                Debug.Log ("Build jar COMPLETE.");
                Debug.Log ("==============================================================");
            }

		}

        static string _destinationPath=null;
		public static void CopyAndroidDirectory (string sourcePath, string destinationPath="",bool isLink = true)
        {
			
        }

		
	#endif
	
	}
