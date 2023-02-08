
using UnityEditor;

[CustomEditor(typeof(GizmosTest))]
public class GizmosTestEditor : Editor
{
	void OnSceneGUI()
	{
		GizmosTest          gizmosTest = (target as GizmosTest);

		// Handles.CapFunction testCapFunction  = (id, position, rotation, size, type) => Debug.Log("Hello cap!");
		// Handles.Button(gizmosTest.transform.position, Quaternion.identity, 10f, 20f, testCapFunction);

		// Handles.DrawDottedLine(gizmosTest.transform.position, Vector3.zero, 5f);
		// Handles.Label(gizmosTest.transform.position, "Hello!");
	}
}
