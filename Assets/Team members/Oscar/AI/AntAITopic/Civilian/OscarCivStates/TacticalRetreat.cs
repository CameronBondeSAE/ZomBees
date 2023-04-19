using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Oscar;
using UnityEngine;

public class TacticalRetreat : AntAIState
{
    private Hearing ears;
    private LittleGuy guy;
    
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);

        guy = aGameObject.GetComponent<LittleGuy>();
        ears = aGameObject.GetComponent<Hearing>();

    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);

        Vector3 runAwayFromLoc = ears.loudestRecentSound;
        
        guy.rb.AddRelativeTorque(0,Vector3.SignedAngle(guy.transform.forward,new Vector3(-runAwayFromLoc.x, runAwayFromLoc.y, -runAwayFromLoc.z), Vector3.up),0);
        guy.rb.AddRelativeForce(Vector3.forward * (guy.speed * 12), ForceMode.Acceleration);
        
    }
}
