interface IInteractable
{
    void Inspect(); 
    void Pickup(); 
}

interface IItem
{
    void Consumed(); 
    void Disposed(); 
    void Discription(); 
}
