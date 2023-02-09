using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, ISwitchable
{
    public bool  state;
    public Light light;

    public void TurnOn()
    {
        light.intensity = 10000;
    }
    public void TurnOff()
    {
        light.intensity = 100;
    }
}
