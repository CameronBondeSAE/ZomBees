using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FlyDudeSpawner))]
public class FlyDudeSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Spawn") && Application.isPlaying)
        {
            (target as FlyDudeSpawner)?.Spawn();
        }
    }
}

[CustomEditor(typeof(GuyDudeSpawnerer))]
public class GuyDudeSpawnererEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        if (GUILayout.Button("Spawn") && Application.isPlaying)
        {
            (target as GuyDudeSpawnerer)?.Spawn();
        }
    }
}
