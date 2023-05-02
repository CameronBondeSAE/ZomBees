using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


using System;
using Oscar;

namespace Virginia
{
    public class Switch : DynamicObject, ISwitchable, IInteractable 
    {
        public event Action TurnOnEvent;
        public event Action TurnOffEvent;

        public ISwitchable ThingToSwitch;
        public StateManager StateManager;
        public List<ISwitchable> SwitchableList = new List<ISwitchable>();

        [Button]  // cheat - plugin
        public void TurnOn()
        {
            StateManager.ChangeState(GetComponent<SwitchOn>());
        }

        [Button] // cheat - plugin
        public void TurnOff()
        {
            StateManager.ChangeState(GetComponent<SwitchOff>());
        }

        public void Interact()
        {
            if (GetComponent<StateManager>().currentState == GetComponent<SwitchOff>())
            {
                StateManager.ChangeState(GetComponent<SwitchOn>());
            }
            else if (GetComponent<StateManager>().currentState == GetComponent<SwitchOn>())
            {
                StateManager.ChangeState(GetComponent<SwitchOff>());
            }
        }
        public void Inspect()
        {}
    }
}
