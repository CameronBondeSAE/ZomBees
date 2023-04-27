using UnityEngine;
using UnityEditor;

namespace Johns
{
    [CustomEditor(typeof(Johns.StateBase),true)]
    public class StateBase_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Change to State"))
            {
                // ‘target’ is the magic variable that editors use to link back to the original component. It’s in the BASE CLASS, so you have to ‘cast’ to get access to YOUR functions.
                StateBase stateBase = target as StateBase;
                stateBase?.GetComponent<StateManager>().ChangeState(stateBase);
            }
        }
    }
}

