using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Virginia;
namespace Virginia
{
    public class SwitchOn : MonoBehaviour
    {
        public Switch Switch;
        
        void OnEnable()
        {
            
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
