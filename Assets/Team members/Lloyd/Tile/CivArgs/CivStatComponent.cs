using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CivStatComponent : MonoBehaviour
{
    #region FloatStats

    [ShowInInspector]
    public float healthDisplayed
    {
        get => Health;
        set => Health = value;
    }

    public float Health
    {
        get => floatMap[CivFloats.health];
        set => floatMap[CivFloats.health] = value;
    }

    [ShowInInspector]
    public float healthThreshDisplayed
    {
        get => HealthThresh;
        set => HealthThresh = value;
    }

    public float HealthThresh
    {
        get => threshMap[CivFloats.health];
        set => threshMap[CivFloats.health] = value;
    }

    [ShowInInspector]
    public bool isAliveDisplayed
    {
        get => IsAlive;
        set => IsAlive = value;
    }

    public bool IsAlive
    {
        get => boolMap[CivFloats.health];
        set => boolMap[CivFloats.health] = value;
    }

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

    [ShowInInspector]
    public float beenessThreshDisplayed
    {
        get => BeenessThresh;
        set => BeenessThresh = value;
    }

    public float BeenessThresh
    {
        get => threshMap[CivFloats.beeness];
        set => threshMap[CivFloats.beeness] = value;
    }

    [ShowInInspector]
    public bool beeDisplayed
    {
        get => Bee;
        set => Bee = value;
    }

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

    [ShowInInspector]
    public float fearThreshDisplayed
    {
        get => FearThresh;
        set => FearThresh = value;
    }

    public float FearThresh
    {
        get => threshMap[CivFloats.fear];
        set => threshMap[CivFloats.fear] = value;
    }

    [ShowInInspector]
    public bool fearedDisplayed
    {
        get => Feared;
        set => Feared = value;
    }

    public bool Feared
    {
        get => boolMap[CivFloats.fear];
        set => boolMap[CivFloats.fear] = value;
    }

    [ShowInInspector]
    public float hungerDisplayed
    {
        get => Hunger;
        set => Hunger = value;
    }

    public float Hunger
    {
        get => floatMap[CivFloats.hunger];
        set => floatMap[CivFloats.hunger] = value;
    }

    [ShowInInspector]
    public float hungerThreshDisplayed
    {
        get => HungerThresh;
        set => HungerThresh = value;
    }

    public float HungerThresh
    {
        get => threshMap[CivFloats.hunger];
        set => threshMap[CivFloats.hunger] = value;
    }

    [ShowInInspector]
    public bool hungry
    {
        get => Hungry;
        set => Hungry = value;
    }

    public bool Hungry
    {
        get => boolMap[CivFloats.hunger];
        set => boolMap[CivFloats.hunger] = value;
    }

    private float minValue = 0.0f;
    private float maxValue = 1.0f;

    public Dictionary<CivFloats, float> floatMap = new Dictionary<CivFloats, float>
    {
        { CivFloats.health, 0.0f },
        { CivFloats.hunger, 0.0f },
        { CivFloats.beeness, 0.0f },
        { CivFloats.fear, 0.0f }
    };

    public Dictionary<CivFloats, float> threshMap = new Dictionary<CivFloats, float>
    {
        { CivFloats.health, 0.0f },
        { CivFloats.hunger, 0.0f },
        { CivFloats.beeness, 0.0f },
        { CivFloats.fear, 0.0f }
    };

    public Dictionary<CivFloats, bool> boolMap = new Dictionary<CivFloats, bool>
    {
        { CivFloats.health, true },
        { CivFloats.beeness, false },
        { CivFloats.fear, false },
        { CivFloats.hunger, false }
    };

    public void Start()
    {
        beenessDisplayed = Beeness;
        beenessThreshDisplayed = BeenessThresh;
        beeDisplayed = Bee;

        fearDisplayed = Fear;
        fearThreshDisplayed = FearThresh;
        fearedDisplayed = Feared;

        hungerDisplayed = Hunger;
        hungerThreshDisplayed = HungerThresh;
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

    [Button]
    public void ChangeStatThreshold(CivFloats floatType, float changeAmount)
    {
        float currentValue = threshMap[floatType];

        float newValue = Mathf.Clamp(currentValue + changeAmount, minValue, maxValue);
        threshMap[floatType] = newValue;

        OnChangeThreshEvent();
    }

    public event Action ChangeThreshEvent;

    public void OnChangeThreshEvent()
    {
        ChangeThreshEvent?.Invoke();
    }

    #endregion

    public string myName;

    public CivCiv myCiv;

    public CivTraits myTraits;
}