using UnityEditor;
using UnityEngine;

namespace Team_members.Lloyd.Scripts_L.Editor
{
    [CustomEditor(typeof(QueenEvent))]
    public class QueenEditor : UnityEditor.Editor
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
}