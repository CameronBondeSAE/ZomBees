using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CivStatComponent : MonoBehaviour
{
    #region FloatStats

    //
    public float beeness;
    public float Beeness
    {
        get => floatMap[CivFloats.beeness];
        set => floatMap[CivFloats.beeness] = value;
    }

    public float beenessThresh;
    public bool bee;

    public float fear
    {
        get => floatMap[CivFloats.fear];
        set => floatMap[CivFloats.fear] = value;
    }

    public float fearThresh;
    public bool feared;

    public float hunger
    {
        get => floatMap[CivFloats.hunger];
        set => floatMap[CivFloats.hunger] = value;
    }

    public float hungerThresh;
    public bool hungry;

    private float minValue = 0.0f;
    private float maxValue = 1.0f;

    private Dictionary<CivFloats, float> floatMap = new Dictionary<CivFloats, float>
    {
        { CivFloats.beeness, 0.0f },
        { CivFloats.fear, 0.0f },
        { CivFloats.hunger, 0.0f }
    };

    private Dictionary<CivFloats, float> threshMap = new Dictionary<CivFloats, float>
    {
        { CivFloats.beeness, 0.0f },
        { CivFloats.fear, 0.0f },
        { CivFloats.hunger, 0.0f }
    };

    private Dictionary<CivFloats, bool> boolMap = new Dictionary<CivFloats, bool>
    {
        { CivFloats.beeness, false },
        { CivFloats.fear, false },
        { CivFloats.hunger, false }
    };

    public void Update()
    {
        beeness = Beeness;
        Debug.Log(beeness);
    }

    [Button]
    public void ChangeStat(CivFloats floatType, float changeAmount)
    {
        float currentValue = floatMap[floatType];

        float newValue = Mathf.Clamp(currentValue + changeAmount, minValue, maxValue);
        floatMap[floatType] = newValue;

        float threshold = threshMap[floatType];
        bool boolToChange = newValue >= threshold;
        boolMap[floatType] = boolToChange;

        OnChangeStatEvent();
    }

    public event Action ChangeStatEvent;

    public void OnChangeStatEvent()
    {
        ChangeStatEvent?.Invoke();
    }

    //

    #endregion

    public HearingComp hearingComp;

    private void OnEnable()
    {
        hearingComp = GetComponent<HearingComp>();
        hearingComp.SoundHeardEvent += OnHeardSound;
    }

    public void OnHeardSound(HearingEventArgs soundArgs)
    {
        ChangeStat(CivFloats.fear, soundArgs.Fear);
    }
}