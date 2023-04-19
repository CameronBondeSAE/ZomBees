using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class NormalCivSuicide : AntAIState
{
    public Transform target;

    public bool hasGun;

    public bool hasExplosive;

    public override void Enter()
    {
        base.Enter();
        
        if(hasGun)
            Debug.Log("Bang!");
        
        if(hasExplosive)
            Debug.Log("Boom!");
        
        else
            Debug.Log("Punch!");
    }
}
