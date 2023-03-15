using System;

public interface IInteractable
{
    public void Interact();
    public void Inspect(); 
    
}

public interface IItem
{
    public void Consume(); 
    public void Dispose(); 
    public void Description();
    public void Pickup(); 
}

public interface ISwitchable
{
    public void TurnOn();
    public void TurnOff();
}
