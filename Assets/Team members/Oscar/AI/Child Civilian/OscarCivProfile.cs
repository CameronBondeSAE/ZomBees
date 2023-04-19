using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class OscarCivProfile : MonoBehaviour
{
    private CivSensor sensor;
    
    [ShowInInspector]
    public Dictionary<string, TraitStats> emoteDictionary;

    public bool hungry;

    public float hungerLevel;
    public float hungerThresh;

    public float beeness;
    public float beenessThresh;

    public bool readyToDie;

    public float suicideLevel;
    public float suicideThresh;

    public bool scared;
    public float fearLevel;
    public float fearThresh;

    private void Start()
    {
        sensor = GetComponent<CivSensor>();
        
        emoteDictionary = new Dictionary<string, TraitStats>();
        
        emoteDictionary.Add("Fear", new TraitStats());
        emoteDictionary.Add("Beeness", new TraitStats());
        emoteDictionary.Add("Hunger", new TraitStats());
        emoteDictionary.Add("Depression", new TraitStats());
    }
    [Button]
    public void UpdateTrait(string key, float value)
    {
        if (emoteDictionary.ContainsKey(key))
        {
            TraitStats traitStats = emoteDictionary[key];
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

            traitStats.value = newValue; 
            
            if (newValue >= traitStats.threshold)
            {
                traitStats.thresholdHit = true;
            }
            else if (newValue < traitStats.threshold)
            {
                traitStats.thresholdHit = false;
            }

            if (key == "Hunger")
            {
                hungerLevel = newValue;
            }

            if (key == "Beeness")
            {
                beeness = newValue;
            }

            if (key == "Fear")
            {
                fearLevel = newValue;
            }
        }
    }
    
    [Button]
    public void OnIncreaseHunger()
    {
        StartCoroutine(IncreaseHunger());
    }

    private IEnumerator IncreaseHunger()
    {
        while (true)
        {
            UpdateTrait("Hunger", .0025f);
            
            yield return new WaitForSeconds(1);
            Debug.Log("Getting hungrier");
            
            if (hungerLevel > hungerThresh)
            {
                hungry = true;
            }

            else
            {
                hungry = false;
            }
        }
    }
    
    [Button]
    public void OnIncreaseBeeness()
    {
        StartCoroutine(IncreaseBeeness());
    }

    private IEnumerator IncreaseBeeness()
    {
        while (true)
        {
            UpdateTrait("Beeness", .1f);
            
            yield return new WaitForSeconds(1);
            Debug.Log("Getting more bee-like");
            
            if (beeness > beenessThresh)
            {
                sensor.BecomeEgg();
            }
        }
    }

    [Button]
    public void OnIncreaseFear()
    {
        StartCoroutine(IncreaseFear());
    }

    private IEnumerator IncreaseFear()
    {
        while (true)
        {
            UpdateTrait("Fear", .4f);
            
            yield return new WaitForSeconds(1);
            Debug.Log("Getting more bee-like");
            
            if (fearLevel > fearThresh)
            {
                scared = true;
            }
            else
            {
                scared = false;
            }
        }
    }
}
