/*using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileLevelMaker))]
public class TileTrackerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        if (GUILayout.Button("SAVE"))
        {
            (target as TileLevelMaker)?.SaveSettings();
        }

        if (GUILayout.Button("LOAD"))
        {
           (target as TileLevelMaker)?.LoadSettings();
        }
        
        if (GUILayout.Button("DISPLAY"))
        {
            (target as TileLevelMaker)?.Board();
        }
    }
}*/