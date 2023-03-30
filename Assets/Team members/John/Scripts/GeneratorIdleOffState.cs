using System.Collections;
using System.Collections.Generic;
using Johns;
using UnityEngine;

public class GeneratorIdleOffState : MonoBehaviour
{
    private bool componentActive = true;
    
    
    public void OnEnable()
    {
        Debug.Log("Phase 1 Begins!");
    }
    
    public void OnDisable()
    {
        Debug.Log("Phase 1 complete!");
    }
    
    public void ToggleActivation()
    {
        componentActive = !componentActive;
        enabled = componentActive;

        if (componentActive)
        {
            OnEnable();
        }
        else
        {
            OnDisable();
        }
    }
}
