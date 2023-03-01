using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Johns;

public class RefferenceScript : MonoBehaviour, ISwitchable
{
    public MonoBehaviour iSwitchable;

    public delegate void DoThing();

    public static event DoThing thingHasBeenDone;

    public void TurnOn()
    {
        if (thingHasBeenDone != null)
        {
            if (iSwitchable)
            {
                thingHasBeenDone();
            }
        }
    }

    public void TurnOff()
    {
        
    }

}
