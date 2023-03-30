using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Virginia
{
    [CustomEditor(typeof(VStateBase),true)]
    public class VStateEditor : Editor
    {
        public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                if (GUILayout.Button("press me"))
                {
                    VStateBase vStateBase = target as VStateBase;
                    vStateBase?.GetComponent<StateManager>().ChangeState(vStateBase);
                }
            }
        

    }
}