using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Oscar
{
    [CustomEditor(typeof(StateManager))]
    public class State_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            #region State 1

            GUILayout.BeginHorizontal();
            GUILayout.Label("state 1");
            if (GUILayout.Button("Start"))
            {
                
            }
            if (GUILayout.Button("Stop"))
            {
                
            }
            GUILayout.EndHorizontal();

            #endregion
            
            #region State 2

            GUILayout.BeginHorizontal();
            GUILayout.Label("state 2");
            if (GUILayout.Button("Start"))
            {
                
            }
            if (GUILayout.Button("Stop"))
            {
                
            }
            GUILayout.EndHorizontal();

            #endregion
            
            #region State 3

            GUILayout.BeginHorizontal();
            GUILayout.Label("state 3");
            if (GUILayout.Button("Start"))
            {
                
            }
            if (GUILayout.Button("Stop"))
            {
                
            }
            GUILayout.EndHorizontal();

            #endregion
            
        }
    }
}

