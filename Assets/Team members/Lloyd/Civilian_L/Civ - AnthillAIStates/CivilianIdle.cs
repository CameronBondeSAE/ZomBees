using System;
using Lloyd;
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