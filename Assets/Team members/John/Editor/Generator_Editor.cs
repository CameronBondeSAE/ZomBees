using System.Collections;
using System.Collections.Generic;
using Johns;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(GeneratorStartingState))]
public class Generator_Starting_Editor : Editor
{
    
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Test Engine Start"))
        {
            // ‘target’ is the magic variable that editors use to link back to the original component. It’s in the BASE CLASS, so you have to ‘cast’ to get access to YOUR functions.
            GeneratorStartingState generatorStartingState = (GeneratorStartingState)target;
            generatorStartingState.ToggleActivation();
        }
    }
}

[CustomEditor(typeof(GeneratorRunningState))]
public class Generator_Running_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Test Engine Running"))
        {
            // ‘target’ is the magic variable that editors use to link back to the original component. It’s in the BASE CLASS, so you have to ‘cast’ to get access to YOUR functions.
            GeneratorRunningState generatorRunningState = (GeneratorRunningState) target;
            generatorRunningState.ToggleActivation();
        }
    }
}

[CustomEditor(typeof(GeneratorShuttingDownState))]
public class _Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Test Generator Shutdown"))
        {
            // ‘target’ is the magic variable that editors use to link back to the original component. It’s in the BASE CLASS, so you have to ‘cast’ to get access to YOUR functions.
            GeneratorShuttingDownState generatorShuttingDownState = (GeneratorShuttingDownState) target;
            generatorShuttingDownState.ToggleActivation();
        }
    }
}
