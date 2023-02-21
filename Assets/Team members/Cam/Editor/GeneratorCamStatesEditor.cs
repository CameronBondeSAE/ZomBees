using UnityEditor;
using UnityEngine;
using Johns;

[CustomEditor(typeof(GeneratorCamStates))]
public class GeneratorCamStatesEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		for (int i = 0; i < 10; i++)
		{
			// GUILayout.BeginVertical();
			GUILayout.BeginHorizontal();
			GUILayout.TextArea("CAM!");
			if (GUILayout.Button("Enter"))
			{
				GeneratorCamStates toggleStateTest = target as GeneratorCamStates;
				// Tell the StateManager to change to THIS state (the target)
				toggleStateTest.GetComponent<StateManager>().ChangeState(toggleStateTest);
			}
			GUILayout.EndHorizontal();
			// GUILayout.EndVertical();
		}
	}
}
