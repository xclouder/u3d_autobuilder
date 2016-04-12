using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class AndroidBuildSettings : ScriptableObject {
	const string SETTING_ASSET_NAME = "AndroidBuildSettings";
	const string SETTING_ASSET_EXT = ".asset";
	const string SETTING_PARENT_DIR = "Assets/AutoBuilder/Editor";
	const string SETTING_PATH = "AutoBuilder/Editor/Resources";

	private static AndroidBuildSettings instance;

	public static AndroidBuildSettings Instance
	{
		get{

			if (instance == null)
			{
				instance = Resources.Load(SETTING_ASSET_NAME) as AndroidBuildSettings;
				if (instance == null)
				{
					//If not exist, autocreate the asset object
					instance = CreateInstance<AndroidBuildSettings>();

					#if UNITY_EDITOR
					string assetPath = Path.Combine(Application.dataPath, SETTING_PATH);
					if (!Directory.Exists(assetPath))
					{
						AssetDatabase.CreateFolder(SETTING_PARENT_DIR, "Resources");
					}

					string fullPath = Path.Combine(Path.Combine("Assets", SETTING_PATH),
						SETTING_ASSET_NAME + SETTING_ASSET_EXT
					);

					AssetDatabase.CreateAsset(instance, fullPath);
					#endif

				}
			}

			return instance;
		}

	}

	#if UNITY_EDITOR
	public static void Edit()
	{
		Selection.activeObject = Instance;
	}

	[SerializeField]
	private string builtJarName = "native.jar";
	public string BuiltJarName {
		get {return builtJarName;}
		set{
			if (builtJarName != value)
			{
				builtJarName = value;
				DirtyEditor();
			}
		}
	}

	protected static void DirtyEditor()
	{
	#if UNITY_EDITOR
		EditorUtility.SetDirty(Instance);
	#endif
	}

	#endif
}

