using System.Collections;
using System.Collections.Generic;
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
            
            GetComponent<SoundEmitter>().EmitSound(soundProperties);
        }

      
        void OnDisable()
        {
            Debug.Log("light is off");
        }
    }
}
