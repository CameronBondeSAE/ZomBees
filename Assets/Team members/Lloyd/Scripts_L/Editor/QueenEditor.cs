using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Lloyd;

[CustomEditor(typeof(QueenEvent))]
public class QueenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("QUEEN ATTACK"))
        {
          //  (target as QueenEvent)?.OnChangeSwarmPoint();
        }

    }
}