using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Virginia;

namespace Virginia { 
public class SwitchOff : VStateBase
{
    public Switch Switch;
        
    void OnEnable()
    {
      //  TurnOffEvent?.Invoke();

            
       Switch.ThingToSwitch?.TurnOff();
       
        GetComponent<Renderer>().material.color = Color.red;
    }

    void OnDisable()
    {
        Debug.Log("light is on");
    }
}
}