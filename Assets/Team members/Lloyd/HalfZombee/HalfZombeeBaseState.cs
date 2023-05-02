using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Lloyd;
using UnityEngine;

public class HalfZombeeBaseState : AntAIState
{
    public HalfZombeeProfile profile;

    public HalfZombeeSensor sensor;
    
    public HalfZombeeTurnTowards turnTowards;
    
    public CivVision vision;

    public Rigidbody rb;
    
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        turnTowards = aGameObject.GetComponent<HalfZombeeTurnTowards>();
        vision = aGameObject.GetComponent<CivVision>();
        profile = aGameObject.GetComponent<HalfZombeeProfile>();
        rb = aGameObject.GetComponent<Rigidbody>();
    }
}
