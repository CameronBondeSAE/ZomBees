using System;
using UnityEngine;

public interface IPowered
{
    public void PoweredOn()
    {
    }

    public void PoweredOff()
    {
    }
}

public interface IInteractable
{
    public void Interact(); // Works on ground and holding
    public void Inspect(); 
    
}

public interface IItem
{
    public void Consume(); // Only works if holding
    public void Dispose(); 
    public string Description();
    public void Pickup(GameObject whoPickedMeUp); //pickup should have reference to a GameObject that its picking up.
}

public interface ISwitchable
{
    public void TurnOn();
    public void TurnOff();
}
