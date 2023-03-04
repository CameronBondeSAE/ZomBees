using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(GeneratorStartingState))]
public class Generator_Starting_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Start the Engine"))
        {
            // ‘target’ is the magic variable that editors use to link back to the original component. It’s in the BASE CLASS, so you have to ‘cast’ to get access to YOUR functions.
            GeneratorStartingState generatorStartingState = (GeneratorStartingState)target;
            generatorStartingState.ToggleActivation();
        }
    }
}

// [CustomEditor(typeof(GeneratorRunningState))]
// public class Generator_Running_Editor : Editor
// {
//     public override void OnInspectorGUI()
//     {
//         base.OnInspectorGUI();
//
//         if (GUILayout.Button("GET BIG"))
//         {
//             // ‘target’ is the magic variable that editors use to link back to the original component. It’s in the BASE CLASS, so you have to ‘cast’ to get access to YOUR functions.
//             CamsDude_Model camsDude_Model;
//             camsDude_Model = target as CamsDude_Model;
//             target?.GetBigOrDieTrying();
//         }
//     }
// }
//
// [CustomEditor(typeof(GeneratorShuttingDownState))]
// public class _Editor : Editor
// {
//     public override void OnInspectorGUI()
//     {
//         base.OnInspectorGUI();
//
//         if (GUILayout.Button("GET BIG"))
//         {
//             // ‘target’ is the magic variable that editors use to link back to the original component. It’s in the BASE CLASS, so you have to ‘cast’ to get access to YOUR functions.
//             CamsDude_Model camsDude_Model;
//             camsDude_Model = target as CamsDude_Model;
//             target?.GetBigOrDieTrying();
//         }
//     }
// }
