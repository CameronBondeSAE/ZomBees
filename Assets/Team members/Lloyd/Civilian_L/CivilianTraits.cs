using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

public class CivilianTraits : MonoBehaviour
{
    [ShowInInspector] public Dictionary<string, TraitStats> traitsDictionary;

    // public void Awake()
    // {
        // floatDictionary = new Dictionary<string, TraitStats>();
    // }

    // [Button]
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

            // floatDictionary[key] = new TraitStats(newValue, traitStats.threshold);
        }
    }
}

public class TraitStats
{
    [Range(0f,1f)]
    public float value = 0.5f;
    [Range(0f, 1f)]
    public float threshold = 1.0f;

    [ReadOnly]
    public bool thresholdHit;

    // public TraitStats(float first, float second)
    // {
        // this.value = first;
        // this.threshold = second;
    // }
}