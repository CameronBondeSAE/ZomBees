using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Johns;

public class RefferenceScript : MonoBehaviour, ISwitchable
{
    public MonoBehaviour iSwitchable;

    public delegate void SwitchOnTheLight();
    public static event SwitchOnTheLight theLightIsSwitchedOn;

    public void TurnOn()
    {
        if (theLightIsSwitchedOn != null)
        {
            theLightIsSwitchedOn();
        }
    }

    public void TurnOff()
    {
        theLightIsSwitchedOn = null;
    }

    public void Toggle()
    {
        throw new System.NotImplementedException();
    }
}
