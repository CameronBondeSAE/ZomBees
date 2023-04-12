using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Virginia;

namespace Virginia { 
public class SwitchOff : VStateBase
{
    public Switch Switch;
        
    void OnEnable()
    {
      //  TurnOffEvent?.Invoke();

            
       Switch.ThingToSwitch?.TurnOff();
       transform.DOLocalMove(new Vector3(-2, 1, -8), 1);
        GetComponent<Renderer>().material.color = Color.red;
    }

    void OnDisable()
    {
        Debug.Log("light is on");
    }
}
}