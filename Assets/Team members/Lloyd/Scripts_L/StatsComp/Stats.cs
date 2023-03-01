using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lloyd;
using Team_members.Lloyd.Scripts_L.StatsComp;

public class Stats : MonoBehaviour
{
    // Stats contains a Dictionary of floats
    // can read / change stat floats through ChangeState
    // ChangeState takes (string variableName, float amount) to change 

    public float min = -1.0f;
    public float max = 1.0f;

    public bool stoic;
    public float anxiety;
    public bool panicked;

    public float beeness;

    public bool holding;
    
    public float minDist;
    
    public float moveSpeed;
    public float maxMoveSpeed;

    private Dictionary<string, float> statValues = new Dictionary<string, float>();

    private StatsModelView modelView;

    private void OnAwake()
    {
        statValues["anxiety"] = anxiety;

        statValues["anxiety"] = beeness;
        
        statValues["minDist"] = minDist;

        statValues["moveSpeed"] = moveSpeed;
        statValues["maxMoveSpeed"] = maxMoveSpeed;
        
        modelView = GetComponentInChildren<StatsModelView>();
        
        modelView.OnSetDictionary(statValues);
    }

    public void ChangeStat(string key, float amount)
    {
        if (statValues.ContainsKey(key))
        {
            statValues[key] += amount;
            statValues[key] = Mathf.Clamp(statValues[key], min, max);
        }

        modelView.OnChangeFloat(key, amount);
    }
}