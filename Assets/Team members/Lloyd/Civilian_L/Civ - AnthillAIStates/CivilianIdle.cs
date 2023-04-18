using System;
using Team_members.Lloyd.Civilian_L;

public class CivilianIdleState : CivModelAIState
{
    public bool idle;

    private void FixedUpdate()
    {
        idle = civBrain.idle;
        
        if(!idle)
            Finish();
    }
    
    
}