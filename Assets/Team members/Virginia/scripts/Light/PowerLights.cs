using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Virginia;

namespace Virginia
{
    public class PowerLights : MonoBehaviour, ISwitchable
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

    }
}
