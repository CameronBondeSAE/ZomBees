using System;
using Oscar;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;
using Virginia;


namespace Johns
{
    public class SoundGunModel : MonoBehaviour, IItem, IPowered, IInteractable, ISwitchable
    { 
        public bool wasPowered;

        public void PoweredOn()
        {
            wasPowered = true;
        }
        
        public void PoweredOff()
        {
            TurnOff();
            wasPowered = false;
        }

        public void TurnOn()
        {
            if (wasPowered)
            {
                GetComponent<StateManager>().ChangeState(GetComponent<SoundGunSwitchedOn>());
            }
        }

        public void TurnOff()
        {
            if (wasPowered == false)
            {
                GetComponent<StateManager>().ChangeState(GetComponent<SoundGunSwitchedOff>());
            }
        }

        public void Toggle()
        {
            throw new NotImplementedException();
        }

        public void Consume()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            UtilityManager.EnableAfterDelay(gameObject);
        }

        public string Description()
        {
            throw new System.NotImplementedException();
        }

        public void Pickup(GameObject whoPickedMeUp)
        {
            UtilityManager.DisableAfterDelay(gameObject,whoPickedMeUp.GetComponent<Inventory>().hand.gameObject);
        }
        
        public void Interact()
        {
            throw new NotImplementedException();
        }

        public void Inspect()
        {
            throw new NotImplementedException();
        }
    } 
}

