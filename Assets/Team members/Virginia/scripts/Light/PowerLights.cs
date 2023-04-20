using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Virginia;

namespace Virginia
{
    public class PowerLights : MonoBehaviour, ISwitchable, IItem
    {
        public Light light;
        public Light2 Light;
        

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
            light2.enabled = true;
        }

        public void TurnOff()
        {
            light.enabled = false;  light2.enabled = false;
        }

    }
}
