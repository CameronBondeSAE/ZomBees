using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Virginia;

namespace Virginia
{
    public class PowerLights : MonoBehaviour, ISwitchable, IItem
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

        public void Consume()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public string Description()
        {
            throw new System.NotImplementedException();
        }

        public void Pickup(GameObject whoPickedMeUp)
        {
            throw new System.NotImplementedException();
        }
    }
}
