namespace Lloyd
{
    public enum NormalCivScenario
    {
        InRange = 0,
        HasInteractTarget = 1,
        WantsToInteract = 2,
    
        HasAttackTarget = 3,
        WantsToAttack = 4,
    
        Idle = 5,
    
        HasMovementTarget = 6,
        HasResource = 7
    }
}