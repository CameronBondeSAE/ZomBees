using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TestScriptableObjects : MonoBehaviour
{
    [FormerlySerializedAs("traitsScriptableObject")]
    public TraitsListScriptableObject traitsListScriptableObject;
    
    [Button]
    public void ShowAllTraits()
    {
        foreach (TraitScriptableObject emotionalCoreEmotion in traitsListScriptableObject.traits)
        {
            Debug.Log(emotionalCoreEmotion.ToString());
        }
    }
}
