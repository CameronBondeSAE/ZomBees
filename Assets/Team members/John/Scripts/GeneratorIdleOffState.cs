using System.Collections;
using System.Collections.Generic;
using Johns;
using UnityEngine;

public class GeneratorIdleOffState : MonoBehaviour
{
    private bool componentActive = true;
    
    
    public void OnEnable()
    {
    }
    
    public void OnDisable()
    {
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
