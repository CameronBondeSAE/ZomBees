using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Virginia;

namespace Virginia { 
public class SwitchOff : MonoBehaviour
{
    public Switch Switch;
    // Start is called before the first frame update
    void OnEnable()
    {
      //  TurnOffEvent?.Invoke();

       Switch.ThingToSwitch?.TurnOff();




        GetComponent<Renderer>().material.color = Color.red;
    }

    // Update is called once per frame
   

    void OnDisable()
    {
        Debug.Log("light is on");
    }
}
}