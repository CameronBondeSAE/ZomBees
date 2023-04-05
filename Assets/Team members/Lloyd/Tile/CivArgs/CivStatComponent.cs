using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CivStatComponent : MonoBehaviour
{
    #region FloatStats

    //
    [ShowInInspector]
    public float beenessDisplayed
    {
        get => Beeness;
        set => Beeness = value;
    }
    public float Beeness
    {
        get => floatMap[CivFloats.beeness];
        set => floatMap[CivFloats.beeness] = value;
    }

    public float beenessThresh;
    public float BeenessThresh
    {
        get => threshMap[CivFloats.beeness];
        set => threshMap[CivFloats.beeness] = value;
    }
    public bool bee;
    public bool Bee
    {
        get => boolMap[CivFloats.beeness];
        set => boolMap[CivFloats.beeness] = value;
    }

    [ShowInInspector]
    public float fearDisplayed
    {
        get => Fear;
        set => Fear = value;
    }
    public float Fear
    {
        get => floatMap[CivFloats.fear];
        set => floatMap[CivFloats.fear] = value;
    }

    public float fearThresh; 
    public float FearThresh
    {
        get => threshMap[CivFloats.fear];
        set => threshMap[CivFloats.fear] = value;
    }
    public bool feared;
    public bool Feared
    {
        get => boolMap[CivFloats.fear];
        set => boolMap[CivFloats.fear] = value;
    }

    public float hunger;
    public float Hunger
    {
        get => floatMap[CivFloats.hunger];
        set => floatMap[CivFloats.hunger] = value;
    }

    public float hungerThresh;
    public float HungerThresh
    {
        get => threshMap[CivFloats.hunger];
        set => threshMap[CivFloats.hunger] = value;
    }
    public bool hungry;
    public bool Hungry
    {
        get => boolMap[CivFloats.hunger];
        set => boolMap[CivFloats.hunger] = value;
    }

    private float minValue = 0.0f;
    private float maxValue = 1.0f;

    public Dictionary<CivFloats, float> floatMap = new Dictionary<CivFloats, float>
    {
        { CivFloats.beeness, 0.0f },
        { CivFloats.fear, 0.0f },
        { CivFloats.hunger, 0.0f }
    };

    public Dictionary<CivFloats, float> threshMap = new Dictionary<CivFloats, float>
    {
        { CivFloats.beeness, 0.0f },
        { CivFloats.fear, 0.0f },
        { CivFloats.hunger, 0.0f }
    };

    public Dictionary<CivFloats, bool> boolMap = new Dictionary<CivFloats, bool>
    {
        { CivFloats.beeness, false },
        { CivFloats.fear, false },
        { CivFloats.hunger, false }
    };

    public void Start()
    {
        //beeness = Beeness;
        beenessThresh = BeenessThresh;
        bee = Bee;
        
        //fear = Fear;
        fearThresh = FearThresh;
        feared = Feared;
        
        hunger = Hunger;
        hungerThresh = HungerThresh;
        hungry = Hungry;
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
        Debug.Log("AHHH");
    }
}