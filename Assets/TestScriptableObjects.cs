using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScriptableObjects : MonoBehaviour
{
    public EmotionalCore emotionalCore;
    
    [Button]
    public void ShowAllEmotions()
    {
        foreach (Emotion emotionalCoreEmotion in emotionalCore._emotions)
        {
            Debug.Log(emotionalCoreEmotion.description + " : "+emotionalCoreEmotion.strength);
        }
    }
}
