using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Virginia;

public class InterfaceLights : MonoBehaviour, ISwitchable
{
    public Light light;
    public Virginia.Switch Vswitch;
    

    public void TurnOn()
    {
        light.enabled = true;
    }

    public void TurnOff()
    {
        light.enabled = false;
    
    }
}
