using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Virginia;

namespace Virginia
{
    public class PowerLights : MonoBehaviour
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

        private void TurnOn()
        {
            light.enabled = true;
        }

        private void TurnOff()
        {
            light.enabled = false;
        }

    }
}
