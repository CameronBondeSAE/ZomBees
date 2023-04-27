using CameronBonde;
using UnityEditor;
using UnityEngine;

namespace CameronBonde
{


	[CustomEditor(typeof(StateBase), true)]
	public class StateEditor : Editor
	{
		
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (GUILayout.Button("Enter"))
			{
				// target points to "Object" classes, BUT since our StateBase script (and everything in Unity)
				// INHERITS from Object, we can check to see if it's a specific type of object
				// Here I'm storing the same memory address of the component, but since it's a defined as a "StateBase" it'll understand everything inside of StateBase
				StateBase stateBase = target as StateBase;
				// Now that the temp variable is SPECIFICALLY pointing to a StateBase I can run things that only work there (Object doesn't have GetComponent for example)
				stateBase?.GetComponent<StateManager>().ChangeState(stateBase);
			}
		}
	}

}