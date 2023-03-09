using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace Virginia
{
    public class Switch : SerializedMonoBehaviour, ISwitchable
    {
        public event Action TurnOnEvent;
        public event Action TurnOffEvent;

        public ISwitchable ThingToSwitch;
        public StateManager StateManager;
        public List<ISwitchable> SwitchableList;

        [Button]  // cheat - plugin
        public void TurnOn()
        {
            foreach (ISwitchable item in SwitchableList)
            {
                item.TurnOn();
                item.TurnOff();
                
            }
            StateManager.ChangeState(GetComponent<SwitchOn>());
            
        }

        [Button] // cheat - plugin
        public void TurnOff()
        {
            StateManager.ChangeState(GetComponent<SwitchOff>());
        }
    }
}
