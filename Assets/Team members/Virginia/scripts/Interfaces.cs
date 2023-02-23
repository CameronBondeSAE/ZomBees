using System;

public interface IInteractable
{
    public void Interact();
    public void Inspect(); 
    public void Pickup(); 
}

public interface IItem
{
    public void Consumed(); 
    public void Disposed(); 
    public void  Description(); 
}

public interface ISwitchable
{
    public void TurnOn();
    public void TurnOff();
}
