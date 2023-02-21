using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Virginia;

public class PowerLights : MonoBehaviour
{
    public Light light;
    public Virginia.Switch Vswitch;
    // Start is called before the first frame update
    void Start()
    {
        Vswitch.TurnOnEvent += TurnOn;
        Vswitch.TurnOffEvent += TurnOff;
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
