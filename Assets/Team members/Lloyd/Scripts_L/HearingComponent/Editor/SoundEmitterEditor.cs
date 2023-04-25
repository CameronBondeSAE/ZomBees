using UnityEditor;
using UnityEngine;

namespace Team_members.Lloyd.Scripts_L.HearingComponent.Editor
{
    [CustomEditor(typeof(SoundEmitter))]
    public class SoundEmitterEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("EMIT SOUND"))
            { 
                (target as SoundEmitter)?.EmitTestSound();
            }

        }
    }
}