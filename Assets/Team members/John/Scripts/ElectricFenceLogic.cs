using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricFenceLogic : MonoBehaviour, ISwitchable
{
    public void TurnOn()
    {
        print("The Fence is now powered on");
    }

    public void TurnOff()
    {
        print("The Fence is now powered off");
    }
}
