using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceTest : MonoBehaviour, ISwitchable, IInteractable
{
	

    public void TurnOn()
    {
	    Debug.Log("InterfaceTest turnon");
    }

    public void TurnOff()
    {
	    Debug.Log("InterfaceTest turnoff");
    }

    public void Toggle()
    {
	    throw new System.NotImplementedException();
    }

    public void Interact()
    {
	    Debug.Log("InterfaceTest interact");
    }

    public void Inspect()
    {
	    
    }
}
