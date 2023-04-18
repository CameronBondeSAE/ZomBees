using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

public class CivilianTraits : SerializedMonoBehaviour
{
    [ShowInInspector] public Dictionary<string, TraitStats> traitsDictionary;
    [ShowInInspector] public List<TraitStats> traits;

    public void UpdateTrait(string key, float value)
    {
        if (traitsDictionary.ContainsKey(key))
        {
            TraitStats traitStats = traitsDictionary[key];
            float oldValue = traitStats.value;
            float newValue = oldValue + value;

            if (newValue > 1.0f)
            {
                newValue = 1.0f;
            }
            else if (newValue < 0.0f)
            {
                newValue = 0.0f;
            }

            if (newValue >= traitStats.threshold)
            {
                traitStats.thresholdHit = true;
            }
            else if (newValue < traitStats.threshold)
            {
                traitStats.thresholdHit = false;
            }
        }
    }
}

public class TraitStats
{
    public TraitScriptableObject traitScriptableObject;

    [Range(0f, 1f)] public float value = 0.5f;
    [Range(0f, 1f)] public float threshold = 1.0f;

    [ReadOnly] public bool thresholdHit;
}