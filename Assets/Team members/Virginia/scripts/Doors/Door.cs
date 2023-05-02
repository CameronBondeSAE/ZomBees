using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.ProBuilder.Shapes;

namespace Virginia
{
    public class Door : DynamicObject, ISwitchable, IInteractable
    {
        
        public StateManager StateManager;
        
       
        [Button] // cheat - plugin
        public void TurnOn()
        {
            StateManager.ChangeState(GetComponent<DoorOpen>());  

        }

        [Button] // cheat - plugin
        public void TurnOff()
        {
            StateManager.ChangeState(GetComponent<DoorClosed>());  

        }

        public void Interact()
        {
            if (GetComponent<StateManager>().currentState == GetComponent<DoorOpen>())
            {
                StateManager.ChangeState(GetComponent<DoorClosed>());
            }
            else if (GetComponent<StateManager>().currentState == GetComponent<DoorClosed>())
            {
                StateManager.ChangeState(GetComponent<DoorOpen>());
            }
        }

        public void Inspect()
        {


        }


    }
}