using UnityEngine;
using System.Collections;
using UnityEditor;



	public class AndroidManifestValueReplacer {

		public void Replace()
		{
		
			//manifest replace
//			string projTemplateFile = "AndroidManifest_Template.xml";
//			string templatePath;
////			if (Config.shareInstance.UseMSDK) {
////				templatePath = System.IO.Path.Combine (Application.dataPath, "Plugins/Android/" + projTemplateFile);
////			} else {
////				templatePath = System.IO.Path.Combine (Application.dataPath, "Plugins/Android/" + projTemplateFile);
////			}
//
//			templatePath = System.IO.Path.Combine (Application.dataPath, "Plugins/Android/" + projTemplateFile);
//
//			string projFile = "AndroidManifest.xml";
//			string manifestPath = System.IO.Path.Combine (Application.dataPath, "Plugins/Android/" + projFile);
//			string directoryPath = System.IO.Path.Combine(Application.dataPath, "Plugins/Android/");
//			
//			Debug.Log ("manifest template Path:" + templatePath);
//			Debug.Log ("manifestPath:" + manifestPath);
//			
//			try
//			{
//				string fileText = System.IO.File.ReadAllText (templatePath);
//				
//				string[] manifestValueToReplace = JDKAndroidBuildSettings.Instance.ManifestValueToReplace;
//				if(manifestValueToReplace!=null){
//					foreach (string replacePair in manifestValueToReplace)
//					{
//						string[] keyValueArr = replacePair.Split('|');
//						
//						if (keyValueArr.Length != 2)
//						{
//							Debug.LogError("invalid keyValue Pair:" + replacePair);
//							continue;
//						}
//						string key = keyValueArr[0];
//						string value = keyValueArr[1];
//						
//						if (fileText.Contains(key))
//						{
//							fileText = fileText.Replace(key, value);
//							Debug.Log("[Manifest Modifier]:replace string:" + key + " to string:" + value);
//						}
//						else
//						{
//							Debug.LogError("key:" + key + " not found, please check your android setting config, now ignore.");
//							continue;
//						}
//						
//					}
//				}
//
////				var defaultMainifestKV = AndroidMainifestKevValues ();
////				foreach(var k in defaultMainifestKV.Keys){
////					fileText = fileText.Replace(k, defaultMainifestKV[k]);
////					Debug.Log("[Manifest Modifier]:replace string:" + k + " to string:" + defaultMainifestKV[k]);
////				}
////				//TODO: 替换config文件的appid
//
//				
//				System.IO.Directory.CreateDirectory(directoryPath);
//				System.IO.File.WriteAllText(manifestPath, fileText);
//				
//				//			fileText.Replace();
//			}
//			catch(System.IO.IOException ioe)
//			{
//				Debug.LogError("read or write manifest file error:" + ioe);
//			}
		}

	}
