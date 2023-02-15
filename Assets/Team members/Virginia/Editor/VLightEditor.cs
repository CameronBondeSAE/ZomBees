using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VLightsEditor))]
public class VLightEditor : Editor
{
   public override void OnInspectorGUI()
    {
       base.OnInspectorGUI();
        if (GUILayout.Button("enter"))
        {

            VLightsEditor toggleStateTest = target as VLightsEditor;
            toggleStateTest.enabled = true;
        }
    }
}
