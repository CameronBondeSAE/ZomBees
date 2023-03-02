using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace Virginia
{
    public class Switch : MonoBehaviour, ISwitchable
    {
        public event Action TurnOnEvent;
        public event Action TurnOffEvent;
       


        [Button]  // cheat - plugin
        public void TurnOn()
        {

            TurnOnEvent?.Invoke();
            
            
                
            GetComponent<Renderer>().material.color = Color.green;

        }

        [Button] // cheat - plugin
        public void TurnOff()
        {
            TurnOffEvent?.Invoke();


          
                
            GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
