using System.Collections;
using System.Collections.Generic;
using Johns;
using UnityEngine;

public class GeneratorIdleOffState : MonoBehaviour, ISwitchable
{
    
    public void TurnOn()
    {
        GetComponent<StateManager>().ChangeState(GetComponent<GeneratorStartingState>());
    }

    public void TurnOff()
    {
        
    }

    public void OnEnable()
    {
    }
    
    public void OnDisable()
    {
    }
}
