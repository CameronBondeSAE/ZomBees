using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(LightEnable))]
public class VLightEditor : Editor
{
   public override void OnInspectorGUI()
    {
       base.OnInspectorGUI();
        if (GUILayout.Button("enter"))
        {

            LightEnable toggleStateTest = target as LightEnable;
            toggleStateTest.enabled = true;
        }
    }
}
