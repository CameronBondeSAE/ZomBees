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

        public ISwitchable ThingToSwitch;

        [Button]  // cheat - plugin
        public void TurnOn()
        {
            TurnOnEvent?.Invoke();

            ThingToSwitch.TurnOn(); 

            GetComponent<Renderer>().material.color = Color.green;

        }

        [Button] // cheat - plugin
        public void TurnOff()
        {
            TurnOffEvent?.Invoke();

            ThingToSwitch.TurnOff();




            GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
