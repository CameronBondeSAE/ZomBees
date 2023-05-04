using System.Collections;
using System.Collections.Generic;
using Oscar;
using UnityEngine;
using Virginia;

namespace Virginia
{
    public class PowerLights : DynamicObject, ISwitchable, IItem
    {
        public Light light;

        public Virginia.Switch Vswitch;

        // Start is called before the first frame update
        void Start()
        {
            if (Vswitch != null)
            {
                Vswitch.TurnOnEvent += TurnOn;
                Vswitch.TurnOffEvent += TurnOff;
            }
        }

        public void TurnOn()
        {
            light.enabled = true;
     
        }

        public void TurnOff()
        {
            light.enabled = false;  
            
        }

        public void Toggle()
        {
            throw new System.NotImplementedException();
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
    }
}
