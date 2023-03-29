using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

public class CivilianEmotions : MonoBehaviour
{
    [ShowInInspector] public Dictionary<string, MyFloatTuple> floatDictionary;

    public float fearFloat;
    public float threshold;
    public bool isFeared = false;

    public void Start()
    {
        floatDictionary = new Dictionary<string, MyFloatTuple>();
        floatDictionary.Add("fear", new MyFloatTuple(fearFloat, threshold, isFeared));
    }

    [Button]
    public void ModifyDictionary(string key, float amount)
    {
        if (floatDictionary.ContainsKey(key))
        {
            MyFloatTuple tuple = floatDictionary[key];
            float oldValue = tuple.emotionFloat;
            float newValue = oldValue + amount;

            if (newValue > 1.0f)
            {
                newValue = 1.0f;
            }
            else if (newValue < 0.0f)
            {
                newValue = 0.0f;
            }

            if (newValue >= tuple.emotionThreshold)
            {
                tuple.emotionBool = true;
            }
            else if (newValue < tuple.emotionThreshold)
            {
                tuple.emotionBool = false;
            }

            floatDictionary[key] = new MyFloatTuple(newValue, tuple.emotionThreshold, tuple.emotionBool);
        }
    }
}

public class MyFloatTuple
{
    public float emotionFloat;
    public float emotionThreshold;
    public bool emotionBool;

    public MyFloatTuple(float first, float second, bool third)
    {
        this.emotionFloat = first;
        this.emotionThreshold = second;
        this.emotionBool = third;
    }
}