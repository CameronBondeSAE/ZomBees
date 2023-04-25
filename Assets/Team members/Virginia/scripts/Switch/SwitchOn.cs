using System.Collections;
using System.Collections.Generic;
using Lloyd;
using UnityEngine;

using Virginia;
namespace Virginia
{
    public class SwitchOn : VStateBase
    {
        public Switch Switch;

        public SoundProperties soundProperties;
        
        void OnEnable()
        {
            
            // foreach (ISwitchable item in SwitchableList)
            // {
            //     item.TurnOn();
            // }
            //TurnOnEvent?.Invoke();
            Switch.ThingToSwitch?.TurnOn();
       
            GetComponent<Renderer>().material.color = Color.green;
        }

      
        void OnDisable()
        {
            Debug.Log("light is off");
        }
    }
}
