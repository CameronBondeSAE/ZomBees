using System;
using UnityEngine;

public interface IInteractable
{
    public void Interact();
    public void Inspect(); 
    
}

public interface IItem
{
    public void Consume(); 
    public void Dispose(); 
    public string Description();
    public void Pickup(GameObject gameObject); //pickup should have reference to a GameObject that its picking up.
}

public interface ISwitchable
{
    public void TurnOn();
    public void TurnOff();
}
