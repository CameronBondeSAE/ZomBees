using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;


namespace Oscar
{
    [CustomEditor(typeof(WorldScanner))]
    public class Pathfinding_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("ScanWorld") && Application.isPlaying)
            {
                (target as WorldScanner)?.ScanWorld();
            }
        }
    }
}
