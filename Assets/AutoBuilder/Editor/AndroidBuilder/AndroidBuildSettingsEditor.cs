using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[CustomEditor(typeof(AndroidBuildSettings))]
public class AndroidBuildSettingsEditor : Editor {

	private AndroidBuildSettings instance;
	SerializedObject m_Object;
	string line = "------------------------------------------------";

	public override void OnInspectorGUI()
	{
		instance = (AndroidBuildSettings)target;
		m_Object = new SerializedObject (instance);

		base.OnInspectorGUI();
	}

	private void AppIdGUI()
	{
		
//		m_Object.Update();
////		EditorGUILayout.LabelField (line);
//		EditorGUILayout.LabelField ("❖❖ Jar name : " +JDKAndroidBuildSettings.Instance.BuiltJarName,EditorStyles.boldLabel);
//		EditorGUILayout.LabelField (line);
//		EditorGUILayout.Space ();
//		EditorGUILayout.Space ();
//		EditorGUILayout.Space ();
////		EditorGUILayout.LabelField (line);
//		EditorGUILayout.LabelField ("❖❖ Default android mainifest key -> values ",EditorStyles.whiteLargeLabel);
//		EditorGUILayout.LabelField (line);
//		GUI.color = Color.green;
//		var kv = JDKDefaultConfig.AndroidMainifestKevValues ();
//		if (kv.Count > 0) {
//			foreach (string key in kv.Keys) {
//				EditorGUILayout.SelectableLabel ("        "+key+" : "+kv [key],GUILayout.Height (18));
//			}
//		}else{
//			EditorGUILayout.LabelField ("        No default android mainifest key -> values.");
//		}
//		GUI.color = Color.white;
//		EditorGUILayout.Space ();
//		EditorGUILayout.Space ();
//		EditorGUILayout.Space ();
////		EditorGUILayout.LabelField (line);
//		EditorGUILayout.LabelField ("❖❖ Custom mainifest key -> values",EditorStyles.whiteLargeLabel);
//		EditorGUILayout.LabelField (line);
//		GUI.color = Color.green;
//		EditorGUILayout.PropertyField(m_Object.FindProperty("manifestValueToReplace"),true);
//		GUI.color = Color.white;
//		m_Object.ApplyModifiedProperties();
	}
}
