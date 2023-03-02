using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class RestartGameObject : EditorWindow
{
	// Add menu named "My Window" to the Window menu
	[MenuItem("Window/Restart GameObject")]
	static void Init()
	{
		// Get existing open window or if none, make a new one:
		RestartGameObject window = (RestartGameObject) EditorWindow.GetWindow(typeof(RestartGameObject));
		window.Show();
	}

	void OnGUI()
	{
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Awake+Start"))
		{
			// Get all the components attached to this game object
			var components = Selection.activeGameObject.GetComponents<Component>();

			// Iterate over the components and call their Awake and Start methods
			foreach (var component in components)
			{
				Type type = component.GetType();

				MethodInfo awakeMethod = type.GetMethod("Awake",
					BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (awakeMethod != null)
				{
					awakeMethod.Invoke(component, null);
				}

				MethodInfo startMethod = type.GetMethod("Start",
					BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (startMethod != null)
				{
					startMethod.Invoke(component, null);
				}
			}
		}

		if (GUILayout.Button("Respawn"))
		{
			// Get the original prefab for the selected game object
			GameObject originalPrefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource(Selection.activeGameObject);

			// Check if the original prefab is a prefab asset
			if (originalPrefab != null)
			{
				if (PrefabUtility.IsPartOfPrefabAsset(originalPrefab))
				{
					// Get the prefab asset path
					var prefabAssetPath = AssetDatabase.GetAssetPath(originalPrefab);
					Debug.Log("Prefab Asset Path:" + prefabAssetPath);
				
					GameObject newGo = Instantiate(originalPrefab, Selection.activeGameObject.transform.position,
						Selection.activeGameObject.transform.rotation);
					DestroyImmediate(Selection.activeGameObject);
					Selection.activeGameObject = newGo;
				}
			}
			else
			{
				Debug.Log("Not a prefab asset, so I'll just clone this one");
				
				GameObject newGo = Instantiate(Selection.activeGameObject, Selection.activeGameObject.transform.position,
					Selection.activeGameObject.transform.rotation);
				DestroyImmediate(Selection.activeGameObject);
				Selection.activeGameObject = newGo;
			}


		}
		GUILayout.EndHorizontal();
	}
}